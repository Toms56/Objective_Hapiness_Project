using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 forest;
    private GameObject homeLumberjack;
    private bool working;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        forest = GameManager.Instance.forestWaypoint.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }

        resident.hobo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day && !working && !resident.tired)
        {
            sleep = false;
            if (transform.position == sleepPos)
            {
                transform.position = homeLumberjack.transform.position + Vector3.left;
                resident.agent.enabled = true;
            }
            resident.agent.SetDestination(forest);
            if (Vector3.Distance(transform.position,forest) <= 1f && !working)            
            {
                StartCoroutine(AddWood());
                resident.tired = true;
                working = true;
            }
        }
        else if (!GameManager.Instance.day && working)
        {
            if (!sleep)
            {
                working = false;
                if (GameManager.Instance.homes.Count == 0)
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    StartCoroutine(resident.Wandering());
                }
                else
                {
                    homeLumberjack = GameManager.Instance.homes[0].gameObject;
                    resident.agent.SetDestination(homeLumberjack.transform.position);
                    Debug.Log("go home");
                }
            }
        }
    }

    IEnumerator AddWood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameManager.wood++;
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
                resident.agent.enabled = false;
                transform.position = sleepPos;
                Debug.Log("sleep");
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeLumberjack = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeLumberjack.transform.position);
                }
                else
                {
                    Debug.Log("wandering");
                    StartCoroutine(resident.Wandering());
                }
            }
        }
    }
}
