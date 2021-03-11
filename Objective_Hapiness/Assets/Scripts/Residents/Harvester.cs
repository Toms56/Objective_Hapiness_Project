using System;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    private H_Resident resident;
    private Vector3 farm;

    private void Awake()
    {
        resident = GetComponent<H_Resident>();
        farm = GameManager.Instance.farmWaypoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(farm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
