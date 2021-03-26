using System.Collections;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    //For deplacement and for working.
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    [SerializeField] Collider2D coll2d;
    private Vector3 farm;
    private GameObject homeHarvester;
    private bool working;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        farm = GameManager.Instance.farmWaypoint.transform.position;
        //Avoid errors.
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        if (coll2d == null)
        {
            coll2d = gameObject.GetComponent<Collider2D>();
        }
        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(farm);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.Instance.day && !working && !resident.tired)
        {
            sleep = false;
            if (transform.position == sleepPos)
            {
                coll2d.enabled = false;
                transform.position = homeHarvester.transform.position;
                resident.agent.enabled = true;
            }
            resident.agent.SetDestination(farm);
            
            if (Vector3.Distance(transform.position,farm) <= 1f && !working)            
            {
                StartCoroutine(AddFood());
                resident.tired = true;
                working = true;
            }
        }
        else if (!GameManager.Instance.day)
        {
            if (!sleep)
            {
                working = false;
                coll2d.enabled = true;
                StopAllCoroutines();
                if (GameManager.Instance.homes.Count == 0)
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                    GameManager.prosperity --;
                }
                else
                {
                    homeHarvester = GameManager.Instance.homes[0].gameObject;
                    resident.agent.SetDestination(homeHarvester.transform.position);
                }
            }
        }
    }
    
    IEnumerator AddFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.food += 1 + GameManager.nbrFarm;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!working)
        {
            if (other.CompareTag(GameManager.Buildings.Home.ToString()))
            {
                if (other.GetComponent<Home>().nbrplace > 0)
                {
                    other.GetComponent<Home>().nbrplace--;
                    resident.tired = false;
                    sleep = true;
                    resident.agent.enabled = false;
                    transform.position = sleepPos;
                    GameManager.prosperity++;
                }
                else
                {
                    if (GameManager.Instance.homes.Count > homeindex)
                    {
                        homeHarvester = GameManager.Instance.homes[homeindex].gameObject;
                        resident.agent.SetDestination(homeHarvester.transform.position);
                    }
                    else
                    {
                        resident.agent.SetDestination(resident.hobWay1);
                        StartCoroutine(resident.Wandering());
                        GameManager.prosperity --;
                    }
                }
            }
        }
    }
}
