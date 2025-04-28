using UnityEngine;
using System.Collections.Generic;
public class Grid : MonoBehaviour
{
    public int width, height;
    public float nodeRadius;
    public Node[,] grid;

    public Grid(int _width, int _height, float _nodeRadius)
    {
        width = _width;
        height = _height;
        nodeRadius = _nodeRadius;
        grid = new Node[width, height];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 worldPoint = new Vector3(x * nodeRadius, 0, z * nodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius); // Check for collision
                grid[x, z] = new Node(worldPoint, walkable);
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / nodeRadius);
        int z = Mathf.FloorToInt(worldPos.z / nodeRadius);
        return grid[x, z];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int x = Mathf.FloorToInt(node.position.x / nodeRadius);
        int z = Mathf.FloorToInt(node.position.z / nodeRadius);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int checkX = x + i;
                int checkZ = z + j;

                if (checkX >= 0 && checkX < width && checkZ >= 0 && checkZ < height)
                {
                    neighbors.Add(grid[checkX, checkZ]);
                }
            }
        }
        return neighbors;
    }
}
