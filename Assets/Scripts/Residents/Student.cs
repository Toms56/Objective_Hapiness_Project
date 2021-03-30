﻿using UnityEngine;

public class Student : MonoBehaviour
{
    //For deplacement and for working.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 school;
    private GameObject homeStudent;
    public int studyDays;
    public GameManager.Works studywork;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private Vector3 studPos = new Vector3(11, 11, 0);
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
        if (spriteresident == null)
        {
            spriteresident = gameObject.GetComponent<SpriteRenderer>();
            spriteresident.color = Color.cyan;
        }
        resident.hobo = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //allows you to configure the number of days that the student
        //will take to learn depending on the job.
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
        if (GameManager.schoolBuilded)
        {
            if (GameManager.day)
            {
                sleep = false;
                homeindex = 1;
            }
            if (GameManager.day && !resident.tired)
            {
                school = GameManager.Instance.school.transform.position;
                if (transform.position == sleepPos) 
                { 
                    transform.position = homeStudent.transform.position; 
                    resident.agent.enabled = true;
                }

                resident.agent.SetDestination(school);
            }
            else if (!GameManager.day && !sleep && resident.tired) 
            {
                sleep = true;
                transform.position = school;
                resident.agent.enabled = true;
                resident.agent.Warp(transform.position);
                resident.StopAllCoroutines();
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
            if (studyDays <= 0)
            {
                transform.position = school;
                resident.agent.enabled = true;
                resident.agent.Warp(transform.position);
                GameManager.Instance.ChangeWork(gameObject,studywork);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.day && !resident.tired && other.CompareTag(GameManager.Buildings.School.ToString()))
        {
            resident.tired = true;
            resident.agent.enabled = false;
            transform.position = studPos;
            studyDays--;
        }
        if (!GameManager.day && resident.tired && other.CompareTag(GameManager.Buildings.Home.ToString()))
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                resident.agent.enabled = false;
                transform.position = sleepPos;
                GameManager.prosperity++;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    resident.agent.enabled = true;
                    homeStudent = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeStudent.transform.position);
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
