using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    void Start()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = GetComponent<NavMeshSurface>();
        }

        BakeNavMesh();
    }

    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}