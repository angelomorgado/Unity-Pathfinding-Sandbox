using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Navigation
{
    public float movementSpeed = 5f;
    private List<Vector3> nodePositions;
    private int currentNodeIndex = 0;
    private Transform transform;

    Astar astar = new Astar();
    NodesOperations nodesOps = new NodesOperations();

    public List<NavMeshNode> Navigate(NavMeshTriangulation navMeshData, Vector3 startPosition, Vector3 destination)
    {
        // get nodes from nav mesh
        List<NavMeshNode> nodes = nodesOps.GetNavMeshNodes(navMeshData);

        NavMeshNode startNode = nodesOps.FindClosestNode(nodes, startPosition);
        NavMeshNode destinationNode = nodesOps.FindClosestNode(nodes, destination);
        List<NavMeshNode> path = astar.FindPath(nodes, startNode, destinationNode);
        return path;

    }

}
