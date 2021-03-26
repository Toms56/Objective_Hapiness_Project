using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    //For deplacement and for working.
    //This script is commented in detail and is valid for Minor and Harvester.
    [SerializeField] H_Resident resident;
    private Vector3 forest;
    private GameObject homeLumberjack;
    private bool working;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        forest = GameManager.Instance.forestWaypoint.transform.position;
        //Avoid errors.
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day && !working && !resident.tired)
        {
            //wakes up the resident and orders him to go to work.
            sleep = false;
            if (transform.position == sleepPos)
            {
                transform.position = homeLumberjack.transform.position + Vector3.left;
                resident.agent.enabled = true;
            }
            resident.agent.SetDestination(forest);
            //Once at the workplace, he adds his resource via a coroutine and becomes tired.
            if (Vector3.Distance(transform.position,forest) <= 1f && !working)            
            {
                StartCoroutine(AddWood());
                resident.tired = true;
                working = true;
            }
        }
        else if (!GameManager.Instance.day && working)
        {
            if (!sleep)
            {
                working = false;
                StopCoroutine(AddWood());
                //if no house is built, the resident wanders.
                if (GameManager.Instance.homes.Count == 0)
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                    GameManager.prosperity --;
                }
                //otherwise he goes to the first house he finds.
                else
                {
                    homeLumberjack = GameManager.Instance.homes[0].gameObject;
                    resident.agent.SetDestination(homeLumberjack.transform.position);
                }
            }
        }
    }

    IEnumerator AddWood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.wood++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameManager.Buildings.Home.ToString()))
        {
            //allows the resident to "sleep" by sending him off the map and removing his tiredness.
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                sleep = true;
                resident.agent.enabled = false;
                transform.position = sleepPos;
            }
            else
            {
                //if the first house is full he goes to the 2nd until time to find space or
                //he ends up wandering if no house is free.
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeLumberjack = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeLumberjack.transform.position);
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
