﻿using System.Collections;
using UnityEngine;

public class Minor : MonoBehaviour
{
    //For deplacement and for working.
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 mine;
    private GameObject homeMinor;
    //private bool working;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        mine = GameManager.Instance.mineWaypoint.transform.position;
        //Avoid errors.
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
                transform.position = homeMinor.transform.position;
                resident.agent.enabled = true;
                resident.agent.Warp(transform.position);
            }
            resident.agent.SetDestination(mine);
        }
        else if (!GameManager.day && !sleep && resident.tired)
        {
            sleep = true;
            StopAllCoroutines();
            if (GameManager.Instance.homes.Count == 0)
            {
                resident.agent.SetDestination(resident.hobWay1);
                GameManager.prosperity --;
            }
            else
            {
                homeMinor = GameManager.Instance.homes[0].gameObject;
                resident.agent.SetDestination(homeMinor.transform.position);
            }
        }
    }
    
    IEnumerator AddStone()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.stone++;
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
                homeMinor = other.gameObject;
                GameManager.prosperity++;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeMinor = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeMinor.transform.position);
                    homeindex++;
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    GameManager.prosperity--;
                }
            }
        }
        if (GameManager.day && !resident.tired && other.CompareTag("Mine"))
        {
            resident.tired = true;
            StartCoroutine(AddStone());
        }
    }
}
