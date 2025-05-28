using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    private NavMeshSurface[] allSurfaces;

    void Start()
    {
        // Find all NavMeshSurface components in the scene
        allSurfaces = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);

        BakeAllNavMeshes();
    }

    public void BakeAllNavMeshes()
    {
        foreach (var surface in allSurfaces)
        {
            surface.BuildNavMesh();
        }
    }
}