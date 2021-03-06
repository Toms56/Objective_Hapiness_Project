﻿using System.Collections;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    //For deplacement and for working.
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 farm;
    private GameObject homeHarvester;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        farm = GameManager.Instance.bushesWaypoint.transform.position;
        //Avoid errors.
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        if (spriteresident == null)
        {
            spriteresident = gameObject.GetComponent<SpriteRenderer>();
            spriteresident.color = Color.green;
        }
        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(resident.hobWay1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.day)
        {
            sleep = false;
            homeindex = 1;
        }
        if (GameManager.day && !resident.tired)
        {
            if (transform.position == sleepPos)
            {
                transform.position = homeHarvester.transform.position;
                resident.agent.enabled = true;
                resident.agent.Warp(transform.position);
            }
            resident.agent.SetDestination(farm);
        }
        else if (!GameManager.day && !sleep && resident.tired)
        {
            sleep = true;
            StopAllCoroutines();
            if (GameManager.Instance.homes.Count == 0)
            {
                resident.agent.SetDestination(resident.hobWay1);
                GameManager.prosperity--;
            }
            else
            {
                homeHarvester = GameManager.Instance.homes[0].gameObject;
                resident.agent.SetDestination(homeHarvester.transform.position);
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
        if (!GameManager.day && resident.tired && other.CompareTag(GameManager.Buildings.Home.ToString()))
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                resident.agent.enabled = false;
                transform.position = sleepPos;
                homeHarvester = other.gameObject;
                GameManager.prosperity++;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    resident.agent.enabled = true;
                    homeHarvester = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeHarvester.transform.position);
                    homeindex++;
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    GameManager.prosperity --;
                }
            }
        }
        if (GameManager.day && !resident.tired && other.CompareTag("Bushes"))
        {
            resident.tired = true;
            StartCoroutine(AddFood());
        }
    }
}
