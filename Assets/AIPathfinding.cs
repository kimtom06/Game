using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;

    private Vector3 startPos;
    private List<Node> path = new List<Node>();
    private Grid grid;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grid = new Grid(10, 10, 1f); // Example grid size (10x10) with 1 unit per grid cell
        startPos = transform.position;
        FindPath(startPos, target.position);
    }

    void Update()
    {
        if (path.Count > 0)
        {
            Vector3 targetPosition = path[0].position;
            Vector3 moveDirection = targetPosition - transform.position;
            rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                path.RemoveAt(0); // Move to next point
            }
        }
    }

    public void FindPath(Vector3 start, Vector3 end)
    {
        Node startNode = grid.GetNodeFromWorldPoint(start);
        Node endNode = grid.GetNodeFromWorldPoint(end);

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost)
                {
                    if (openSet[i].hCost < currentNode.hCost)
                        currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                float newGCost = currentNode.gCost + Vector3.Distance(currentNode.position, neighbor.position);
                if (newGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = Vector3.Distance(neighbor.position, endNode.position);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        finalPath.Reverse();
        path = finalPath;
    }
}
