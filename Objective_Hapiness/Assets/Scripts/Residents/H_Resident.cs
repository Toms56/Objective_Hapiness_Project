using System.Collections.Generic;
using UnityEngine;

public class H_Resident : MonoBehaviour
{
    #region Variables
    
    // For deplacement
    [SerializeField] private float speed;
    private bool move;
    private float startTime;
    private Vector3Int Vactual;
    private Vector3Int Vtarget;
    private List<Vector3Int> listMove = new List<Vector3Int>();
    private Node Nstart;    
    private Node Ntarget;
    
    // For parameters' resident
    private int age = 20;
    protected bool tired;
    private bool hobo;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day)
        {
            tired = true;
        }
        
        if (age >= 70)
        {
            Destroy(gameObject);
        }
    }
    
    
    #region Pathfinding
    // allows to transform the list of nodes into a list of vectors which can be used for the movement of the ghost.
    public virtual List<Vector3Int> NodelisttoVectorlist(List<Node> listnode)
    { List<Vector3Int> vectorlist = new List<Vector3Int>();
        for (int i = 0; i < listnode.Count; i++)
        { vectorlist.Add(listnode[i].position); }
        return vectorlist; }
    // Recursive method of BFS (Breadth First Search) to have the shortest path between
    // 2 nodes located in the dictionary.    
    public virtual List<Node> DisplayPath(Node firstNode, Node lastNode)
    { //Allows to reset the pathfinding.
        foreach (Vector3Int coord in GameManager.Instance.dictio.Keys)
        { GameManager.Instance.dictio[coord].parent = null;
            GameManager.Instance.dictio[coord].depth = -1; }
        //create a queue for nodes.
        Queue<Node> NodeQueue = new Queue<Node>();
        //the queue is done from first node.
        NodeQueue.Enqueue(firstNode); 
        firstNode.depth = 0;
        while (NodeQueue.Count > 0)
        { //removes a node from the queue.
            Node myNode = NodeQueue.Dequeue();
            //explore all the possibilities of paths via the neighbours of the nodes until find lastnode.
            foreach (Node n in myNode.neighbours)
            { //allows you to go through this node only once to avoid having to reproduce 
                //the same path ad infinitum and explore strictly different paths.
                if (n.depth == -1)
                { //add the neighbor node to the queue.
                    NodeQueue.Enqueue(n);
                    n.depth = myNode.depth + 1;
                    n.parent = myNode;
                    if (n == lastNode)      
                    { //Reverse path from lastnode to firstnode via parents to get the shortest path.
                        //Then we reverse this list so that the ghost follows this path by returning this list.
                        List<Node> path = GetParentPath(lastNode);
                        path.Reverse();
                        return path; } } } }
        return new List<Node>(); }
    public virtual List<Node> GetParentPath(Node node, List<Node> path = null)
    { if(path == null)
        { path = new List<Node>(); }
        path.Add(node);
        if (node.parent == null)
        { return path; }
        else
        { return GetParentPath(node.parent, path); } }
    #endregion
}
