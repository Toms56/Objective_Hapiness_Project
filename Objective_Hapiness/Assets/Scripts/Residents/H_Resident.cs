using UnityEngine;
using UnityEngine.AI;

public class H_Resident : MonoBehaviour
{
    #region Variables
    
    
    
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

    
}
