using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    NodesOperations nodesOps = new NodesOperations();

    public List<NavMeshNode> FindPath(List<NavMeshNode> allNodes, NavMeshNode startNode, NavMeshNode finalNode)
    {
        // Create open and closed lists
        List<NavMeshNode> openList = new List<NavMeshNode>();
        List<NavMeshNode> closedList = new List<NavMeshNode>();

        // Assign initial values to start node
        startNode.G = 0;
        startNode.H = CalculateHeuristic(startNode, finalNode);
        startNode.F = startNode.G + startNode.H;

        // Add start node to open list
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // Get the node with the lowest F cost from the open list
            NavMeshNode currentNode = nodesOps.GetNodeWithLowestFCost(openList);

            // Remove the current node from the open list and add it to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // If the current node is the final node, a path has been found
            if (currentNode == finalNode)
            {
                return ConstructPath(currentNode);
            }

            // Get the neighbor nodes of the current node
            List<NavMeshNode> neighbors = nodesOps.GetNeighborNodes(currentNode, allNodes);

            foreach (NavMeshNode neighbor in neighbors)
            {
                // Skip neighbor nodes that are already in the closed list
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                // Calculate the cost to move to the neighbor node from the current node
                float movementCost = currentNode.G + CalculateDistance(currentNode, neighbor);

                // If the neighbor node is not in the open list, or the new path to it is better,
                // update its costs and add it to the open list
                if (!openList.Contains(neighbor) || movementCost < neighbor.G)
                {
                    neighbor.G = movementCost;
                    neighbor.H = CalculateHeuristic(neighbor, finalNode);
                    neighbor.F = neighbor.G + neighbor.H;
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        // No path found
        return null;
    }


    private float CalculateHeuristic(NavMeshNode node, NavMeshNode finalNode)
    {
        // Calculate the heuristic value between a node and the final node (you can use any appropriate heuristic calculation method)
        return Vector3.Distance(node.vertexA, finalNode.vertexA);
    }

    private float CalculateDistance(NavMeshNode nodeA, NavMeshNode nodeB)
    {
        // Calculate the distance between two nodes (you can use any appropriate distance calculation method)
        return Vector3.Distance(nodeA.vertexA, nodeB.vertexA);
    }

    private List<NavMeshNode> ConstructPath(NavMeshNode finalNode)
    {
        List<NavMeshNode> path = new List<NavMeshNode>();
        NavMeshNode currentNode = finalNode;

        while (currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = currentNode.Parent;
        }

        return path;
    }

}
