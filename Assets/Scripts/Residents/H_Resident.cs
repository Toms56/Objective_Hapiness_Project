using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class H_Resident : MonoBehaviour
{
    #region Variables
    
    // For deplacement
    [SerializeField] private float speed;
    public NavMeshAgent agent;
    public Vector3 hobWay1;
    public Vector3 hobWay2;
    

    // For parameters' resident
    public int age = 20;
    public bool tired;
    public bool hobo = true;
    private bool getolder;
    
    #endregion

    void Awake()
    {
        if (agent == null)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        hobWay1 = GameManager.Instance.hoboWaypoint1.transform.position;
        hobWay2 = GameManager.Instance.hoboWaypoint2.transform.position;
        //if the resident is a hobo, he will wander between 2 points.
        if (hobo)
        {
            agent.SetDestination(hobWay1);
            StartCoroutine(Wandering());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the resident is too old he is "killed" by being deactivated
        //and reset to be put in the queue of the pool manager.
        if (age >= 70)
        {
            ResetToHobo();
        }

        #region GetOlder
        if (!GameManager.day && !getolder)
        {
            getolder = true;
            age += 5;
        }
        if (GameManager.day && getolder)
        {
            getolder = false;
        }
        if (!hobo)
        {
            StopCoroutine(Wandering());
        }
        #endregion
        
    }

    public IEnumerator Wandering()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position,hobWay1) < 2f)
            { agent.SetDestination(hobWay2); }
            if (Vector3.Distance(transform.position,hobWay2) < 2f)
            { agent.SetDestination(hobWay1); }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ResetToHobo()
    {
        GameObject gmobj = gameObject;
        switch (gmobj.tag)
        {
            case "Builder" :
                Destroy(gmobj.GetComponent<Builder>());
                break;
            case "Harvester":
                Destroy(gmobj.GetComponent<Harvester>());
                break;
            case "Lumberjack":
                Destroy(gmobj.GetComponent<Lumberjack>());
                break;
            case "Minor":
                Destroy(gmobj.GetComponent<Minor>());
                break;
            case "Hobo":
                break;
            default:
                Debug.Log("no tag");
                break;
        }
        age = 20;
        tired = false;
        hobo = true;
        gmobj.name = GameManager.Works.Hobo.ToString();
        gmobj.tag = GameManager.Works.Hobo.ToString();
        PoolManager.inactiveResidents.Enqueue(gmobj);
        gmobj.SetActive(false);
    }
}
