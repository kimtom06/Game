using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIPathfinding : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float cellSize = 1f;
    public LayerMask wallMask;

    private Rigidbody rb;
    private Queue<Vector3> pathQueue = new Queue<Vector3>();
    private Vector3 targetCell;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(UpdatePath), 0f, 1f); // Update path every 1 second
    }

    void FixedUpdate()
    {
        if (pathQueue.Count > 0)
        {
            Vector3 nextPos = pathQueue.Peek();
            Vector3 dir = (nextPos - transform.position).normalized;
            rb.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, nextPos) < 0.1f)
                pathQueue.Dequeue();
        }
    }

    void UpdatePath()
    {
        Vector3 start = RoundToCell(transform.position);
        Vector3 end = RoundToCell(target.position);
        if (end != targetCell)
        {
            targetCell = end;
            pathQueue = BFS(start, end);
        }
    }

    Queue<Vector3> BFS(Vector3 start, Vector3 goal)
    {
        Queue<Vector3> frontier = new Queue<Vector3>();
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        frontier.Enqueue(start);
        cameFrom[start] = start;

        Vector3[] directions = new Vector3[]
        {
            Vector3.forward * cellSize,
            Vector3.back * cellSize,
            Vector3.right * cellSize,
            Vector3.left * cellSize
        };

        while (frontier.Count > 0)
        {
            Vector3 current = frontier.Dequeue();

            if (current == goal)
                break;

            foreach (var dir in directions)
            {
                Vector3 next = current + dir;
                if (!cameFrom.ContainsKey(next) && !Physics.CheckBox(next, Vector3.one * (cellSize / 2f), Quaternion.identity, wallMask))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        var path = new Queue<Vector3>();
        if (!cameFrom.ContainsKey(goal)) return path;

        Vector3 curr = goal;
        while (curr != start)
        {
            path.Enqueue(curr);
            curr = cameFrom[curr];
        }

        var reversed = new Queue<Vector3>();
        foreach (var pos in new List<Vector3>(path)) reversed.Enqueue(pos);
        return new Queue<Vector3>(new Stack<Vector3>(path));
    }

    Vector3 RoundToCell(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x / cellSize) * cellSize,
            pos.y,
            Mathf.Round(pos.z / cellSize) * cellSize
        );
    }
}
