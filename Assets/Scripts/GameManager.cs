using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    // The instance allows all variables declared in the game manager
    // to exist only once in the scene and remain unique.
    public static GameManager Instance;
    
    public static float prosperity;
    
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
    public static int food = 50;
    public static int wood = 50;
    public static int stone = 50;

    public static int nbrFarm = 0;
    public static int nbrBuilder = 0;

    public NavMeshSurface2d surface2d;
    public GameObject hoboWaypoint1;
    public GameObject hoboWaypoint2;
    public GameObject farmWaypoint;
    public GameObject mineWaypoint;
    public GameObject forestWaypoint;
    public GameObject school;
    
    public List<GameObject> homes;

    
    
    public enum Works
    {
        Builder, Harvester, Lumberjack, Minor, Student, Hobo
    }

    public enum Buildings
    {
        Home, School, Farm, Librairy, Museum, Construction
    }
    
    //World settings
    private int timeWorld;
    public static bool day;
    public static bool schoolBuilded;
    public static bool gameOver;
    public static bool victory;

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
        day = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region Resources Management
        //prevent the resources number to be less than 0
        if (food <= 0)
        { food = 0; }
        if (wood <= 0)
        { wood = 0; }
        if (stone <= 0)
        { stone = 0; }
        #endregion
        
        //CheatCode
        if (Input.GetKey(KeyCode.Insert))
        {
            prosperity = 95;
        }

        if (prosperity >= 100)
        {
            victory = true;
        }
    }

    public void RebuildSurface()
    {
        surface2d.BuildNavMesh();
    }
    
    //With ToStudy (), this function manages the automatic change of the resident
    //to another profession thanks to a switch and this function is only carried out
    //when a student has learned his profession allowing to be little greedy in resources.
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
    //allows to transform a resident into a student by deleting his previous trade
    //and that he learns the new one according to a certain number of days.
    public void ToStudy(GameObject resident, Works work)
    {
        switch (resident.tag)
        { 
            case "Builder" :
                Destroy(resident.GetComponent<Builder>());
                break;
            case "Harvester":
                Destroy(resident.GetComponent<Harvester>());
                break;
            case "Lumberjack":
                Destroy(resident.GetComponent<Lumberjack>());
                break;
            case "Minor":
                Destroy(resident.GetComponent<Minor>());
                break;
            case "Hobo":
                PoolManager.activeResidents.Add(resident);
                break;
            default:
                Debug.LogError("no tag");
                break; 
        }
        resident.name = Works.Student.ToString();
        resident.tag = Works.Student.ToString();
        resident.GetComponent<H_Resident>().hobo = false;
        resident.AddComponent<Student>().studywork = work;
    }
}
