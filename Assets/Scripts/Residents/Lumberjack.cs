using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    //This script is commented in detail and is valid for Minor, Builder,
    //Student and Harvester with rare exceptions.
    
    //For deplacement and for working.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 forest;
    private GameObject homeLumberjack;
    private int homeindex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private bool sleep;

    private void Awake()
    {
        forest = GameManager.Instance.forestWaypoint.transform.position;
        //Avoid errors
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        if (spriteresident == null)
        {
            spriteresident = gameObject.GetComponent<SpriteRenderer>();
            spriteresident.color = new Color(0.4f,0.2f,0f);
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
        //Avoid errors for sleep time.
        if (GameManager.day)
        {
            sleep = false;
            homeindex = 1;
        }
        if (GameManager.day && !resident.tired)
        {
            //wakes up the resident and orders him to go to work.
            if (transform.position == sleepPos)
            {
                transform.position = homeLumberjack.transform.position;
                resident.agent.enabled = true;
            }
            //goes to his workplace
            resident.agent.SetDestination(forest);
        }
        else if (!GameManager.day && !sleep && resident.tired)
        {
            StopAllCoroutines();
            sleep = true;
            //if no house is built, the resident wanders.
            if (GameManager.Instance.homes.Count == 0)
            {
                resident.agent.SetDestination(resident.hobWay1);
                //StartCoroutine(resident.Wandering());
                GameManager.prosperity --;
            }
            //otherwise he goes to the first house he finds.
            else
            {
                homeLumberjack = GameManager.Instance.homes[0].gameObject;
                resident.agent.SetDestination(homeLumberjack.transform.position);
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
        if (other.CompareTag(GameManager.Buildings.Home.ToString()) && !GameManager.day && resident.tired)
        {
            //allows the resident to "sleep" by sending him off the map and removing his tiredness.
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
                //if the first house is full he goes to the 2nd until time to find space or
                //he ends up wandering if no house is free.
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    homeLumberjack = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeLumberjack.transform.position);
                    homeindex++;
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    GameManager.prosperity--;
                }
            }
        }
        //Once at the workplace, he adds his resource via a coroutine and becomes tired.
        if (GameManager.day && !resident.tired && other.CompareTag("Forest"))
        {
            resident.tired = true;
            StartCoroutine(AddWood());
        }
    }
}
