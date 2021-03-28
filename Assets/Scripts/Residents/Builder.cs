﻿using UnityEngine;

public class Builder : MonoBehaviour
{
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    private Vector3 building;
    private GameObject homeBuilder;
    private bool working;
    private int homeindex = 1;
    private int constructionIndex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;
    private Vector3 positionConstruction;
    SpriteRenderer construcSprite; 

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
        resident.agent.speed = Random.Range(1f, 3f);
        GameManager.nbrBuilder++;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.day && !working && !resident.tired)
        {
            //wakes up the resident and orders him to go to work.
            sleep = false;

            if (transform.position == sleepPos)
            {
                transform.position = homeBuilder.transform.position;
                resident.agent.enabled = true;
                GameManager.nbrBuilder++;
            }

            if (BuildingManager.dictoConstructions.Count > 0)
            {
                //GameManager.nbrBuilder++;
                foreach (Vector3 buildpose in BuildingManager.dictoConstructions.Keys)
                {
                    //Debug.Log("Buildpose1 : " + BuildingManager.dictoConstructions[buildpose]);
                    if (BuildingManager.dictoConstructions[buildpose] > 0)
                    {
                        working = true;
                        building = buildpose;
                        resident.agent.SetDestination(building);
                        //Debug.Log("Buildpose1 : " + BuildingManager.dictoConstructions[buildpose]);
                    }
                    else
                    {
                        resident.agent.SetDestination(resident.hobWay1);
                        StartCoroutine(resident.Wandering());
                    }
                }
            }
            else
            {
                resident.agent.SetDestination(resident.hobWay1);
                StartCoroutine(resident.Wandering());
            }
        }
        else if (!GameManager.day && working && !sleep && resident.tired)
        {
            working = false;
            if (GameManager.Instance.homes.Count == 0)
            {
                resident.agent.SetDestination(resident.hobWay1);
                StartCoroutine(resident.Wandering());
                GameManager.prosperity--;
            }
            else
            {
                homeindex = 1;
                homeBuilder = GameManager.Instance.homes[0].gameObject;
                resident.agent.SetDestination(homeBuilder.transform.position);
            }
        }
        else if (GameManager.day && working && !resident.tired)
        {
            if (construcSprite !=null && construcSprite.color.a >= 1)
            {
                resident.tired = true;
                transform.position = positionConstruction;
                resident.agent.enabled = true;
                resident.agent.SetDestination(resident.hobWay1);
                StartCoroutine(resident.Wandering());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameManager.Buildings.Construction.ToString()))
        {
            if (BuildingManager.dictoConstructions.ContainsKey(other.transform.position))
            {
                if (BuildingManager.dictoConstructions[other.transform.position] > 0)
                {
                    BuildingManager.dictoConstructions[other.transform.position]--;
                    resident.agent.enabled = false;
                    transform.position = sleepPos;
                    GameManager.nbrBuilder--;
                    construcSprite = other.gameObject.GetComponent<SpriteRenderer>();
                    positionConstruction = other.transform.position;
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                }
            }
        }
        else
        {
            if (BuildingManager.dictoConstructions.Count > constructionIndex)
            {
                foreach (Vector3 buildpose in BuildingManager.dictoConstructions.Keys)
                {
                    if (BuildingManager.dictoConstructions[buildpose] > 0)
                    {
                        building = buildpose;
                        resident.agent.SetDestination(resident.hobWay1);
                        resident.agent.SetDestination(building);
                    }
                }
            }
            else
            {
                resident.agent.SetDestination(resident.hobWay1);
                StartCoroutine(resident.Wandering());
            }
        }
        if (!working && !sleep && other.CompareTag(GameManager.Buildings.Home.ToString()))
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                GameManager.nbrBuilder++;
                sleep = true;
                resident.agent.enabled = false;
                transform.position = sleepPos;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeBuilder = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeBuilder.transform.position);
                    homeindex++;
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
