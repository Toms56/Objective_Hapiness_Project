using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 build;
    private GameObject homeBuilder;
    private bool building;
    public static List<Vector3> constructList = new List<Vector3>();

    // Faire une static sur ce script en bool et l'incorporer sur le building

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
        GameManager.nbrBuilder++;
        /*if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(build);
            
            if (Vector3.Distance(transform.position,build) <= 1f && !building)
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
        Debug.Log("Liste de Transform : " + constructList.Count);
    }
}
