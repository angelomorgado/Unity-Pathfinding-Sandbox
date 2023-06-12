using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Agent : MonoBehaviour
{
    public Transform target;
    public Transform transform;
    private NavMeshAgent navMeshAgent;
    private Navigation navigation;
    private NodesOperations nodesOps;
    private List<Vector3> nodePositions;
    private int currentNodeIndex = 0;
    public float movementSpeed = 5f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navigation = new Navigation();
        nodesOps = new NodesOperations();
    }

    private void Start()
    {
        if (target != null)
        {
            CalculatePath(target.position);
        }
    }

    public void CalculatePath(Vector3 destination)
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Vector3 startPosition = transform.position;
        
        List<NavMeshNode> path = navigation.Navigate(navMeshData, startPosition, destination);
        MoveAlongNodes(path);
    }

    public void MoveAlongNodes(List<NavMeshNode> nodes)
    {
        // Store the positions of the nodes
        nodePositions = new List<Vector3>();
        foreach (NavMeshNode node in nodes)
        {
            nodePositions.Add(nodesOps.GetNodeCenter(node));
        }

        // Start moving towards the first node
        StartCoroutine(MoveToNextNode());
    }

    private IEnumerator MoveToNextNode()
    {
        while (currentNodeIndex < nodePositions.Count)
        {
            Vector3 targetPosition = nodePositions[currentNodeIndex];
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }

            currentNodeIndex++;
        }

        // Reached the end of the nodes
        // You can perform any desired action here
        Debug.Log("Reached the end of the nodes");
    }

}