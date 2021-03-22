using UnityEngine;
using UnityEngine.AI;

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
    public GameObject home;
    
    public enum Works
    {
        Builder,
        Harvester,
        Lumberjack,
        Minor,
        Student,
        Hobo
    }

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
    
    public void ChangeWork(GameObject resident, Works work)
    {
        switch (work)
        {
            case Works.Builder :
                resident.name = Works.Builder.ToString();
                resident.tag = Works.Builder.ToString();
                resident.GetComponent<H_Resident>().hobo = false;
                resident.AddComponent<Builder>();
                break;
            case Works.Harvester : 
                resident.name = Works.Harvester.ToString();
                resident.tag = Works.Harvester.ToString();
                resident.GetComponent<H_Resident>().hobo= false;
                resident.AddComponent<Harvester>();
                break;
            case Works.Lumberjack :
                resident.name = Works.Lumberjack.ToString();
                resident.tag = Works.Lumberjack.ToString();
                resident.GetComponent<H_Resident>().hobo = false;
                resident.AddComponent<Lumberjack>();
                break;
            case Works.Minor :
                resident.name = Works.Lumberjack.ToString();
                resident.tag = Works.Lumberjack.ToString();
                resident.GetComponent<H_Resident>().hobo = false;
                resident.AddComponent<Minor>();
                break;
            default:
                Debug.LogError("pas de métier");
                break;
        }
    }
    
}
