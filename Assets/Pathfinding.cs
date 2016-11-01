using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    public Transform seeker;
    public Transform target;

   void Awake()
   {
       grid = GetComponent<Grid>();
   }

    void Update()
    {
        FindPath(seeker.position,target.position);
    }

    void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.nodeFromWorldPoint(startPosition);
        Node targetNode = grid.nodeFromWorldPoint(targetPosition);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();

            closeSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode,targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbors(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }

                int newMoveCostToNeighbours = currentNode.gCost + getDistance(currentNode, neighbour);
                if (newMoveCostToNeighbours < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMoveCostToNeighbours;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);

                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Math.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Math.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14*distanceY + 10*(distanceX - distanceY);
        }

        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
