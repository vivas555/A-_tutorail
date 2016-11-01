using UnityEngine;
using System.Collections;

public class Node : IHeapitem<Node>
{

    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public Node parent;
    public int HeapIndex { get; set; }


    public Node(bool _walkable, Vector3 _worldposition, int _gridx, int _gridy)
    {
        walkable = _walkable;
        worldPosition = _worldposition;
        gridX = _gridx;
        gridY = _gridy;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }

   
}
