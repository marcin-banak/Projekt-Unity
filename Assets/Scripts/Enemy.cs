using UnityEngine;
using UnityEngine.AI;

public class RandomDestination : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        // Get the NavMeshAgent component attached to the same GameObject
        agent = GetComponent<NavMeshAgent>();

        // Set the first random destination when the script starts
        SetRandomDestination();
    }

    void Update()
    {
        // Check if the agent has reached its destination
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // Set a new random destination when the agent reaches the current one
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Get a random point on the NavMesh
        Vector3 randomPoint = GetRandomPointOnNavMesh();

        // Set the agent's destination to the random point
        agent.SetDestination(randomPoint);
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        // Generate a random point within the NavMesh bounds
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int randomIndex = Random.Range(0, navMeshData.vertices.Length);

        return navMeshData.vertices[randomIndex];
    }
}