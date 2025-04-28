using UnityEngine;
public class Node
{
    public Vector3 position;
    public bool walkable;
    public float gCost;
    public float hCost;
    public Node parent;

    public float FCost { get { return gCost + hCost; } }

    public Node(Vector3 _position, bool _walkable)
    {
        position = _position;
        walkable = _walkable;
    }
}
