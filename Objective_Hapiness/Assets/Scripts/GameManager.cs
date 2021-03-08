using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    #region Variables

    // The instance allows all variables declared in the game manager
    // to exist only once in the scene and remain unique.
    // The tilemap is that of the black tiles which are the displacement tiles and
    // the dictionnary contains a position vector associated with a node having the same position vector.
    public static GameManager Instance;
    public Tilemap tilemapPath;
    public Dictionary<Vector3Int, Node> dictio = new Dictionary<Vector3Int, Node>();

    //World settings
    private int timeworld;
    public bool day;
    public bool schoolbuilded;
    
    // Tilemap bounds
    BoundsInt bounds;
    // List containing all the possible vectors for the neighbours of a node.
    private List<Vector3Int> directions = new List<Vector3Int> 
        {Vector3Int.down,Vector3Int.left,Vector3Int.right,Vector3Int.up};

    #endregion
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        bounds = tilemapPath.cellBounds;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        DetectNodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region NodesPathfinding
    // Detects all black tiles and associates their position
    // With a position vector in the dictionary as well as a node.
    private void DetectNodes()
    { for (int x = bounds.xMin; x < (bounds.xMax); x++)
        { for (int y = (bounds.yMin); y < bounds.yMax; y++)
            { TileBase tile = tilemapPath.GetTile(new Vector3Int(x, y, 0));
                if (tile != null) 
                { dictio.Add(new Vector3Int(x, y, 0), new Node(new Vector3Int(x, y, 0))); } } } 
        DetectNeighbours(); }
    // Detects the neighbors of each node contained in the dictionary and adds them to this node.
    // Thanks to the Debug.DrawLine we visualize all the nodes and their neighbors.
    private void DetectNeighbours()
    { foreach (Vector3Int coord in dictio.Keys)
        { foreach (Vector3Int dir in directions)
            { if (dictio.ContainsKey(coord + dir))
                { dictio[coord].neighbours.Add(dictio[coord + dir]);
                    Debug.DrawLine(tilemapPath.layoutGrid.GetCellCenterWorld(coord),
                        tilemapPath.layoutGrid.GetCellCenterWorld(coord +dir ), Color.red, 10 ); } } } }
    #endregion
}
