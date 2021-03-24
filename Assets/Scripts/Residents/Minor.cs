﻿using System.Collections;
using UnityEngine;

public class Minor : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 mine;
    private GameObject homeMinor;

    private bool working;

    private void Awake()
    {
        mine = GameManager.Instance.mineWaypoint.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(mine);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(mine);
            
            if (Vector3.Distance(transform.position,mine) <= 1f && !working)
            {
                StartCoroutine(AddStone());
                working = true;
            }
        }
        else
        {
            working = false;
            resident.agent.SetDestination(homeMinor.transform.position);
        }
    }



    IEnumerator AddStone()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.Instance.stone++;
        }
    }
}
