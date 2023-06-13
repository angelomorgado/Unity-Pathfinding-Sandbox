using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public enum AlgoEnum
{
    Astar,
    Dijkstra
}


public class Agent : MonoBehaviour
{
    public Transform target;
    public AlgoEnum algo;
    private NavMeshAgent navMeshAgent;
    private Navigation navigation;
    private NodesOperations nodesOps;
    private List<Vector3> nodePositions;
    private int currentNodeIndex = 0;
    public float movementSpeed = 5f;
    public bool isMoving = false; 
    private Animator animator;
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(PlayerPrefs.GetInt("AlgorithmChoice", 0) == 0)
            algo = AlgoEnum.Astar;
        else
            algo = AlgoEnum.Dijkstra;
        
        navigation = new Navigation(algo);
        nodesOps = new NodesOperations();

        // Get animator from children
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            CalculatePath(target.position);
        }
    }

    public void CalculatePath(Vector3 destination)
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Vector3 startPosition = transform.position;
        
        List<NavMeshNode> path = navigation.Navigate(navMeshData, startPosition, destination);
        MoveAlongNodes(path, destination);
    }

    public void MoveAlongNodes(List<NavMeshNode> nodes, Vector3 destination)
    {
        // Store the positions of the nodes
        nodePositions = new List<Vector3>();
        foreach (NavMeshNode node in nodes)
        {
            nodePositions.Add(nodesOps.GetNodeCenter(node));
        }

        // Start moving towards the first node
        StartCoroutine(MoveToNextNode(destination));

        // Walk animation
        animator.SetTrigger("Walk");
    }

    private IEnumerator MoveToNextNode(Vector3 destination)
    {
        while (currentNodeIndex < nodePositions.Count)
        {
            Vector3 targetPosition = nodePositions[currentNodeIndex];
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
                yield return null;
            }

            currentNodeIndex++;
        }

        // Reached the end of the nodes
        // Apply noise to the target position
        Vector3 finalTargetPosition = nodePositions[nodePositions.Count - 1];
        Vector3 noise = new Vector3(
            Random.Range(-1.0f, 1.0f),
            0.0f,
            Random.Range(-1.0f, 1.0f)
        );
        Vector3 noisyTargetPosition = finalTargetPosition + noise;

        // Move towards the noisy target position
        while (transform.position != noisyTargetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, noisyTargetPosition, movementSpeed * Time.deltaTime);
            transform.LookAt(target.position);
            yield return null;
        }

        // Perform any desired action after reaching the final position with noise
        Debug.Log("Reached the end of the nodes with noise");
        isMoving = false;
        currentNodeIndex = 0;
    }
}