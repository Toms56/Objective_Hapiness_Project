﻿using UnityEngine;

public class Builder : MonoBehaviour
{
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 building;
    private GameObject homeBuilder;
    private bool working;
    private int homeindex = 1;
    private int constructionIndex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;
    SpriteRenderer construcSprite;
    private bool wandering;

    private void Awake()
    {
        //build = GameManager.Instance.school.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }

        if (spriteresident == null)
        {
            spriteresident = gameObject.GetComponent<SpriteRenderer>();
            spriteresident.color = Color.gray;
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
            sleep = false;

            if (transform.position == sleepPos)
            {
                transform.position = homeBuilder.transform.position;
                resident.agent.enabled = true;
                GameManager.nbrBuilder++;
            }

            if (BuildingManager.dictoConstructions.Count > 0)
            {
                SearchConstruction();
            }
            else
            {
                if (!wandering)
                {
                    wandering = true;
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                }
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
                transform.position = construcSprite.transform.position;
                resident.agent.enabled = true;
                resident.agent.SetDestination(resident.hobWay1);
                StartCoroutine(resident.Wandering());
            }
        }
    }

    private void SearchConstruction()
    {
        foreach (Vector3 buildpose in BuildingManager.dictoConstructions.Keys)
        {
            if (BuildingManager.dictoConstructions[buildpose] > 0)
            {
                constructionIndex = 1;
                building = buildpose;
                resident.agent.SetDestination(building);
            }
            else
            {
                if (BuildingManager.dictoConstructions.Count > constructionIndex)
                {
                    foreach (Vector3 buildpose2 in BuildingManager.dictoConstructions.Keys)
                    {
                        if (BuildingManager.dictoConstructions[buildpose2] > 0)
                        {
                            building = buildpose2;
                            resident.agent.SetDestination(building);
                            constructionIndex++;
                        }
                    }
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                }
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
                    working = true;
                    transform.position = sleepPos;
                    GameManager.nbrBuilder--;
                    construcSprite = other.gameObject.GetComponent<SpriteRenderer>();
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                }
            }
        }
        
        if (other.CompareTag(GameManager.Buildings.Home.ToString()) && !GameManager.day && resident.tired  && !working && !sleep)
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                sleep = true;
                resident.agent.enabled = false;
                transform.position = sleepPos;
                wandering = false;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    resident.agent.enabled = true;
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
