using System;
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
        {
            agent.SetDestination(hobWay1);
            StartCoroutine(Wandering());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (age >= 70)
        {
            ResetToHobo();
        }

        if (!hobo)
        {
            StopCoroutine(Wandering());
        }
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
        switch (gameObject.tag)
        {
            case "Builder" :
                Destroy(gameObject.GetComponent<Builder>());
                break;
            case "Harvester":
                Destroy(gameObject.GetComponent<Harvester>());
                break;
            case "Lumberjack":
                Destroy(gameObject.GetComponent<Lumberjack>());
                break;
            case "Minor":
                Destroy(gameObject.GetComponent<Minor>());
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
        gameObject.name = GameManager.Works.Hobo.ToString();
        gameObject.tag = GameManager.Works.Hobo.ToString();
        PoolManager.Instance.inactiveResidents.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
