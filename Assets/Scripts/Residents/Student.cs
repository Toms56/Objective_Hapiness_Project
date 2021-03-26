﻿using UnityEngine;

public class Student : MonoBehaviour
{
    //For deplacement and for working.
    [SerializeField] H_Resident resident;
    private Vector3 school;
    private GameObject homeStudent;
    private bool studying;
    public int studyDays;
    public GameManager.Works studywork;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;
    private bool boolstud;

    private void Awake()
    {
        school = GameManager.Instance.school.transform.position;
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
        switch (studywork)
        {
            case GameManager.Works.Builder :
                studyDays = 2;
                break;
            case GameManager.Works.Harvester : 
                studyDays = 3;
                break;
            case GameManager.Works.Lumberjack :
                studyDays = 4;
                break;
            case GameManager.Works.Minor :
                studyDays = 1;
                break;
            default:
                Debug.LogError("pas de métier à étudier");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.schoolBuilded)
        {
            if (studyDays <= 0)
            {
                GameManager.Instance.ChangeWork(gameObject,studywork);
            }
            if (!GameManager.Instance.day && !boolstud)
            {
                boolstud = true;
                studyDays --;
            } 
            if (GameManager.Instance.day && boolstud)
            {
                boolstud = false;
            }
                    
            if (GameManager.Instance.day && !studying && !resident.tired)
            { 
                sleep = false; 
                if (transform.position == sleepPos) 
                { 
                    transform.position = homeStudent.transform.position + Vector3.left; 
                    resident.agent.enabled = true;
                } 
                resident.agent.SetDestination(school);
                if (Vector3.Distance(transform.position,school) <= 1f && !studying)            
                {
                    resident.tired = true; 
                    studying = true;
                }
            }
            else if (!GameManager.Instance.day && studying) 
            {
                if (!sleep)
                {
                    studying = false;
                    if (GameManager.Instance.homes.Count == 0)
                    {
                        resident.agent.SetDestination(resident.hobWay1);
                        StartCoroutine(resident.Wandering());
                        GameManager.prosperity --;
                    }
                    else
                    {
                        homeStudent = GameManager.Instance.homes[0].gameObject;
                        resident.agent.SetDestination(homeStudent.transform.position);
                    }
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameManager.Buildings.Home.ToString()))
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                sleep = true;
                GameManager.prosperity++;
                resident.agent.enabled = false;
                transform.position = sleepPos;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeStudent = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeStudent.transform.position);
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
