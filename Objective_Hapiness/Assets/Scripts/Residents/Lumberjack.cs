using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 forest;
    private GameObject homeLumberjack;
    private bool working;

    private void Awake()
    {
        forest = GameManager.Instance.forestWaypoint.transform.position;
        homeLumberjack = GameManager.Instance.home;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(forest);
            
            if (Vector3.Distance(transform.position,forest) <= 0.5f && !working)            
            {
                StartCoroutine(AddWood());
                working = true;
            }
        }
        else
        {
            working = false;
            resident.agent.SetDestination(homeLumberjack.transform.position);
        }
    }

    IEnumerator AddWood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.Instance.wood++;
        }
    }
}
