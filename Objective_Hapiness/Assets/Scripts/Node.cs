using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> neighbours;
    public Vector3Int position;
    public Node parent;
    public int depth;


    //the node class to declare each path node with a position vector
    //and we can later implement a list of neighbours and using parent
    //to achieve BFS to find the shortest path.
    public Node(Vector3Int position)
    {
        this.position = position;
        this.depth = -1;
        this.parent = null;
        this.neighbours = new List<Node>(); 
    }
}
