using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshNode
{
    public int index;
    public Vector3 vertexA;
    public Vector3 vertexB;
    public Vector3 vertexC;
    public float F;  // Total cost (F = G + H)
    public float G;  // Cost from start to current node
    public float H;  // Heuristic cost from current node to goal
    public NavMeshNode Parent;  // Parent node in the path

    public NavMeshNode(int index, Vector3 vertexA, Vector3 vertexB, Vector3 vertexC)
    {
        this.index = index;
        this.vertexA = vertexA;
        this.vertexB = vertexB;
        this.vertexC = vertexC;
    }
}

public class NodesOperations
{
    public List<NavMeshNode> GetNavMeshNodes(UnityEngine.AI.NavMeshTriangulation navMeshData)
    {
        // Access the vertices and indices
        Vector3[] vertices = navMeshData.vertices;
        int[] indices = navMeshData.indices;
        
        // Create an array to store the nodes
        List<NavMeshNode> nodes = new List<NavMeshNode>();
        
        // Process the nodes
        for (int i = 0; i < indices.Length; i += 3)
        {
            // Get the triangle vertices
            Vector3 vertexA = vertices[indices[i]];
            Vector3 vertexB = vertices[indices[i + 1]];
            Vector3 vertexC = vertices[indices[i + 2]];
            
            // Create a NavMeshNode object
            NavMeshNode node = new NavMeshNode(i / 3, vertexA, vertexB, vertexC);
            
            // Add the node to the array
            nodes.Add(node);
        }
        
        return nodes;
    }

    public List<NavMeshNode> GetNeighborNodes(NavMeshNode node, List<NavMeshNode> allNodes)
    {
        List<NavMeshNode> neighbors = new List<NavMeshNode>();

        foreach (NavMeshNode otherNode in allNodes)
        {
            if (node != otherNode && AreNodesNeighbors(node, otherNode))
            {
                neighbors.Add(otherNode);
            }
        }

        return neighbors;
    }

    private bool AreNodesNeighbors(NavMeshNode nodeA, NavMeshNode nodeB)
    {
        // Check if the nodes share at least one edge (vertex pair)
        bool hasSharedEdge = false;

        if (nodeA.vertexA == nodeB.vertexA || nodeA.vertexA == nodeB.vertexB || nodeA.vertexA == nodeB.vertexC)
        {
            hasSharedEdge = true;
        }
        else if (nodeA.vertexB == nodeB.vertexA || nodeA.vertexB == nodeB.vertexB || nodeA.vertexB == nodeB.vertexC)
        {
            hasSharedEdge = true;
        }
        else if (nodeA.vertexC == nodeB.vertexA || nodeA.vertexC == nodeB.vertexB || nodeA.vertexC == nodeB.vertexC)
        {
            hasSharedEdge = true;
        }

        return hasSharedEdge;
    }

    public NavMeshNode GetNodeWithLowestFCost(List<NavMeshNode> nodes)
    {
        NavMeshNode lowestNode = nodes[0];

        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].F < lowestNode.F)
            {
                lowestNode = nodes[i];
            }
        }

        return lowestNode;
    }

    public NavMeshNode FindClosestNode(List<NavMeshNode> allNodes, Vector3 position)
    {
        NavMeshNode closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (NavMeshNode node in allNodes)
        {
            float distance = Vector3.Distance(position, GetNodeCenter(node));

            if (distance < closestDistance)
            {
                closestNode = node;
                closestDistance = distance;
            }
        }

        return closestNode;
    }

    public Vector3 GetNodeCenter(NavMeshNode node)
    {
        // Calculate the center position of the node (you can use any appropriate calculation method)
        return (node.vertexA + node.vertexB + node.vertexC) / 3f;
    }

}
