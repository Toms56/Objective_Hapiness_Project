﻿using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 build;
    private GameObject homeBuilder;
    private bool building;


    private void Awake()
    {
        build = GameManager.Instance.school.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.nbrBuilder++;
        /*if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(build);
            
            if (Vector3.Distance(transform.position,build) <= 0.5f && !building)
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
        
    }
}
