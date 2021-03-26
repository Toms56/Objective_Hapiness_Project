using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 building;
    private GameObject homeBuilder;
    private bool working;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    // Faire une static sur ce script en bool et l'incorporer sur le building

    private void Awake()
    {
        //build = GameManager.Instance.school.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.nbrBuilder++;
        /*if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(build);
            
            if (Vector3.Distance(transform.position,build) <= 1f && !building)
            {
                building = true;
            }
        }
        else
        {
            building = false;
            resident.agent.SetDestination(homeBuilder.transform.position);
        }*/
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
                transform.position = homeBuilder.transform.position + Vector3.left;
                resident.agent.enabled = true;
            }
            if (BuildingManager.dictoConstructions.Count > 0)
            {
                foreach(Vector3 buildpose in BuildingManager.dictoConstructions.Keys)
                {
                    //Debug.Log("Buildpose1 : " + BuildingManager.dictoConstructions[buildpose]);
                    if (BuildingManager.dictoConstructions[buildpose] > 0)
                    {
                        building = buildpose;
                        //Debug.Log("Buildpose1 : " + BuildingManager.dictoConstructions[buildpose]);
                    }
                }
            }
            
            resident.agent.SetDestination(building);
            //Once at the workplace, he adds his resource via a coroutine and becomes tired.
            if (Vector3.Distance(transform.position, building) <= 1f && !working)
            {
                resident.tired = true;
                working = true;
            }
        }
        else if (!GameManager.Instance.day && working)
        {
            if (!sleep)
            {
                working = false;
                //if no house is built, the resident wanders.
                if (GameManager.Instance.homes.Count == 0)
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                    GameManager.prosperity--;
                }
                //otherwise he goes to the first house he finds.
                else
                {
                    homeBuilder = GameManager.Instance.homes[0].gameObject;
                    resident.agent.SetDestination(homeBuilder.transform.position);
                }
            }
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
                    homeBuilder = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeBuilder.transform.position);
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                    GameManager.prosperity--;
                }
            }
        }
    }
}
