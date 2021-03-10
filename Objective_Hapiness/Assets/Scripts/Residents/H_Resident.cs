using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_Resident : MonoBehaviour
{
    #region Variables
    
    // For deplacement
    [SerializeField] private float speed;
    private bool move;
    private float startTime;
    private Vector3Int vectorActual;
    private Vector3Int vectorTarget;
    private List<Vector3Int> listMove = new List<Vector3Int>();
    private List<Vector3Int> listMoveSave = new List<Vector3Int>();
    private Node nodeStart;    
    private Node nodeTarget;
    private Grid grid;
    private Dictionary<Vector3Int, Node> pathDictio;
    private bool switchBool;
    
    //UI Management
    public Text ageText;
    public Text jobText;

    // For parameters' resident
    private int age = 20;
    protected bool tired;
    protected bool hobo;
    
    #endregion

    private void Awake()
    {
        grid = GameManager.Instance.tilemapPath.layoutGrid;
        pathDictio = GameManager.Instance.dictio;
        nodeStart = pathDictio[grid.WorldToCell(GameManager.Instance.hoboWaypoint1.transform.position)];
        nodeTarget = pathDictio[grid.WorldToCell(GameManager.Instance.hoboWaypoint2.transform.position)];
        listMoveSave = NodelisttoVectorlist(DisplayPath(nodeStart, nodeTarget));
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        hobo = true;
        nodeStart = pathDictio[grid.WorldToCell(transform.position)];
        nodeTarget = pathDictio[grid.WorldToCell(GameManager.Instance.hoboWaypoint1.transform.position)];
        listMove = NodelisttoVectorlist(DisplayPath(nodeStart, nodeTarget));
        vectorActual = listMove[0];
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (GameManager.Instance.day)
        {
            tired = true;
        }

        if (hobo)
        {
            if (listMove.Count <= 0)
            {
                if (!switchBool)
                {
                    listMove = listMoveSave;
                    switchBool = true;
                }
                else
                {
                    listMove = listMoveSave;
                    listMove.Reverse();
                    vectorActual = listMove[0];
                    switchBool = false;
                }
            }
            // !move allows him to finish his move and listMove.Count> 0 avoids list errors.
            if (!move && listMove.Count > 0)
            {
                startTime = Time.time;
                vectorTarget = listMove[0];
                move = true;
            }
            float actualTime = Time.time - startTime;
            float percent = actualTime * speed;
            transform.position = Vector3.Lerp(grid.GetCellCenterWorld(vectorActual), grid.GetCellCenterWorld(vectorTarget),percent);

            if (percent >= 1 && listMove.Count > 0)
            {
                listMove.RemoveAt(0);
                move = false;
                vectorActual = vectorTarget;
            }
        }
        
        if (age >= 70)
        {
            gameObject.SetActive(false);
        }

        #region TextDisplay

        if (gameObject != null)
        {
            ageText.text = "Score : " + age;
            jobText.text = "Job : " + gameObject.name;
        }

        #endregion
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
