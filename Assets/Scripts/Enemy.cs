using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    private NavMeshAgent agent;
    public float followTime;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        followTime = 0;
        SetRandomDestination();
    }

    void Update() {
        if (followTime > 0) {
            agent.SetDestination(playerTransform.position);
            followTime -= Time.deltaTime;
        }
        if (!agent.pathPending && agent.remainingDistance < 0.1f) {
            SetRandomDestination();
        }
    }

    void SetRandomDestination() {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        agent.SetDestination(randomPoint);
    }

    Vector3 GetRandomPointOnNavMesh() {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.vertices.Length);
        return navMeshData.vertices[randomIndex];
    }
}