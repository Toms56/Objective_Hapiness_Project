using System.Collections;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 farm;
    private GameObject homeHarvester;

    private bool working;


    private void Awake()
    {
        farm = GameManager.Instance.farmWaypoint.transform.position;
        homeHarvester = GameManager.Instance.home;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(farm);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(farm);
            
            if (Vector3.Distance(transform.position,farm) <= 0.5f && !working)
            {
                StartCoroutine(AddFood());
                working = true;
            }
        }
        else
        {
            working = false;
            resident.agent.SetDestination(homeHarvester.transform.position);
        }
    }



    IEnumerator AddFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.Instance.food += 1 + GameManager.Instance.nbrFarm;
        }
        
    }
}
