using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Navigation
{

    Astar astar = new Astar();
    Dijkstra dijkstra = new Dijkstra();
    NodesOperations nodesOps = new NodesOperations();
    private AlgoEnum algo;

    public Navigation(AlgoEnum enumValue)
    {
        algo = enumValue;
    }

    public List<NavMeshNode> Navigate(NavMeshTriangulation navMeshData, Vector3 startPosition, Vector3 destination)
    {
        // get nodes from nav mesh
        List<NavMeshNode> nodes = nodesOps.GetNavMeshNodes(navMeshData);

        NavMeshNode startNode = nodesOps.FindClosestNode(nodes, startPosition);
        NavMeshNode destinationNode = nodesOps.FindClosestNode(nodes, destination);
        List<NavMeshNode> path;
        if(algo == AlgoEnum.Dijkstra){
            path = dijkstra.FindPath(nodes, startNode, destinationNode);
        }
        else{
            path = astar.FindPath(nodes, startNode, destinationNode);
        }
        return path;

    }

}