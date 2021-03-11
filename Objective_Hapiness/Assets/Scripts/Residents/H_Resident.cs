using UnityEngine;
using UnityEngine.AI;

public class H_Resident : MonoBehaviour
{
    #region Variables
    
    public enum Works
    {
        Builder,
        Harvester,
        Lumberjack,
        Minor,
        Student,
        Hobo
    }
    
    // For deplacement
    [SerializeField] private float speed;
    public NavMeshAgent agent;
    private Vector3 hobWay1;
    private Vector3 hobWay2;
    

    // For parameters' resident
    public int age = 20;
    public bool tired;
    public bool hobo;
    
    #endregion

    void Awake()
    {
        hobWay1 = GameManager.Instance.hoboWaypoint1.transform.position;
        hobWay2 = GameManager.Instance.hoboWaypoint2.transform.position;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        if (hobo)
        { agent.SetDestination(hobWay1); }
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
            gameObject.SetActive(false);
        }

        if (hobo)
        {
            if (Vector3.Distance(transform.position,hobWay1) < 0.8f)
            { agent.SetDestination(hobWay2); }
            if (Vector3.Distance(transform.position,hobWay2) < 0.8f)
            { agent.SetDestination(hobWay1); }
        }
    }

    public void ChangeWork(GameObject resident, Works work)
    {
        switch (work)
        {
            case Works.Builder :
                resident.name = "Builder";
                resident.tag = "Builder";
                resident.AddComponent<Builder>();
                break;
            case Works.Harvester : 
                resident.name = "Harvester";
                resident.tag = "Harvester";
                resident.AddComponent<Harvester>();
                break;
            case Works.Lumberjack :
                resident.name = "Lumberjack";
                resident.tag = "Lumberjack";
                resident.AddComponent<Lumberjack>();
                break;
            case Works.Minor :
                resident.name = "Minor";
                resident.tag = "Minor";
                resident.AddComponent<Minor>();
                break;
            default:
                Debug.LogError("pas de métier");
                break;
        }
    }
}
