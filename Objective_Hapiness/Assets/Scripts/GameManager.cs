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
    public Tilemap tilemap;
    public Dictionary<Vector3Int, Node> dictio = new Dictionary<Vector3Int, Node>();

    //World settings
    private int timeworld;
    public bool day;

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
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
