using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    // The instance allows all variables declared in the game manager
    // to exist only once in the scene and remain unique.
    public static GameManager Instance;
    
    public float prosperity;
    
    //PNJ Comportment Management 
    /*
     * public bool isEating;
     */
    
    //TimeManagement
    /*
     * public float dayTime;
     * public int dayCycle;
     */
    
    //Resources management 
    public int food;
    public int wood;
    public int stone;

    public NavMeshSurface2d surface2d;
    public GameObject hoboWaypoint1;
    public GameObject hoboWaypoint2;
    public GameObject farmWaypoint;
    public GameObject mineWaypoint;
    public GameObject forestWaypoint;


    [SerializeField] GameObject poolManager;

    //World settings
    private int timeWorld;
    public bool day;
    public bool schoolBuilded;

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
        Instantiate(poolManager);
    }

    // Update is called once per frame
    void Update()
    {
        #region Resources Management
        //prevent the resources number to be less than 0
        if (food <= 0)
        {
            food = 0;
        }
        if (wood <= 0)
        {
            wood = 0;
        }
        if (stone <= 0)
        {
            stone = 0;
        }

        #endregion
        //CheatCode
        if (Input.GetKey(KeyCode.Insert))
        {
            prosperity = 95;
        }
    }

    public void GoToEat()
    {
        
    }

    public void RebuildSurface()
    {
        surface2d.BuildNavMesh();
    }
    
}
