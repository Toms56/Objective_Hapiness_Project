using UnityEngine;

public class Builder : MonoBehaviour
{
    //This script is commented in detail on Lumberjack.
    [SerializeField] H_Resident resident;
    private SpriteRenderer spriteresident;
    private Vector3 building;
    private GameObject homeBuilder;
    private int homeindex = 1;
    private int constructionIndex = 1;
    private Vector3 sleepPos = new Vector3(10, 10, 0);
    private Vector3 buildPos = new Vector3(-10, 10, 0);
    private bool sleep;
    SpriteRenderer construcSprite;
    private bool wandering;
    private bool isconstruc;

    private void Awake()
    {
        if (spriteresident == null)
        {
            spriteresident = gameObject.GetComponent<SpriteRenderer>();
            spriteresident.color = Color.yellow;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
        if (!resident.tired)
        {
            GameManager.nbrBuilder++;
            
        }
        resident.agent.SetDestination(resident.hobWay1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.day)
        {
            sleep = false;
            homeindex = 1;
        } 
        if (GameManager.day && !resident.tired)
        {
            GoToWork();
        }
        else if (!GameManager.day && !sleep && resident.tired)
        {
            GoToSleep();
        }
        if (construcSprite !=null && construcSprite.color.a >= 1 && !resident.tired)
        {
            resident.tired = true;
            transform.position = construcSprite.transform.position;
            resident.agent.enabled = true;
            resident.agent.SetDestination(resident.hobWay1);
            construcSprite = null;
        }
    }

    private void GoToWork()
    {
        if (transform.position == sleepPos)
        {
            transform.position = homeBuilder.transform.position;
            resident.agent.enabled = true;
            resident.agent.Warp(transform.position);
            GameManager.nbrBuilder++;
            wandering = false;
            isconstruc = false;
        }
        if (BuildingManager.dictoConstructions.Count > 0 && !isconstruc)
        {
            SearchConstruction();
        }
        else
        {
            if (!wandering)
            {
                wandering = true;
                resident.agent.enabled = true;
                resident.agent.Warp(transform.position);
                resident.agent.SetDestination(resident.hobWay1);
            }
        }
    }

    private void GoToSleep()
    {
        sleep = true;
        if (GameManager.Instance.homes.Count == 0)
        {
            resident.agent.SetDestination(resident.hobWay1);
            GameManager.prosperity--;
        }
        else
        {
            homeBuilder = GameManager.Instance.homes[0].gameObject;
            resident.agent.enabled = true;
            resident.agent.Warp(transform.position);
            resident.agent.SetDestination(homeBuilder.transform.position);
        }
    }
    
    private void SearchConstruction()
    {
        foreach (Vector3 buildpose in BuildingManager.dictoConstructions.Keys)
        {
            if (BuildingManager.dictoConstructions[buildpose] > 0)
            {
                constructionIndex = 1;
                building = buildpose;
                resident.agent.enabled = true;
                resident.agent.SetDestination(building);
            }
            else
            {
                if (BuildingManager.dictoConstructions.Count > constructionIndex)
                {
                    foreach (Vector3 buildpose2 in BuildingManager.dictoConstructions.Keys)
                    {
                        if (BuildingManager.dictoConstructions[buildpose2] > 0)
                        {
                            building = buildpose2;
                            resident.agent.enabled = true;
                            resident.agent.SetDestination(building);
                            constructionIndex++;
                        }
                    }
                }
                else
                {
                    if (!wandering)
                    {
                        wandering = true;
                        resident.agent.SetDestination(resident.hobWay1);
                        //StartCoroutine(resident.Wandering());
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameManager.Buildings.Construction.ToString()))
        {
            if (BuildingManager.dictoConstructions.ContainsKey(other.transform.position))
            {
                if (BuildingManager.dictoConstructions[other.transform.position] > 0)
                {
                    BuildingManager.dictoConstructions[other.transform.position]--;
                    resident.agent.enabled = false;
                    transform.position = buildPos;
                    isconstruc = true;
                    construcSprite = other.gameObject.GetComponent<SpriteRenderer>();
                }
                else
                {
                    resident.agent.Warp(transform.position);
                    resident.agent.SetDestination(resident.hobWay1);
                }
            }
        }
        
        if (other.CompareTag(GameManager.Buildings.Home.ToString()) && !GameManager.day && resident.tired && sleep)
        {
            if (other.GetComponent<Home>().nbrplace > 0)
            {
                other.GetComponent<Home>().nbrplace--;
                resident.tired = false;
                resident.agent.enabled = false;
                transform.position = sleepPos;
                wandering = false;
                GameManager.prosperity++;
            }
            else
            {
                if (GameManager.Instance.homes.Count > homeindex)
                {
                    resident.agent.enabled = true;
                    homeBuilder = GameManager.Instance.homes[homeindex].gameObject;
                    resident.agent.SetDestination(homeBuilder.transform.position);
                    homeindex++;
                }
                else
                {
                    resident.agent.SetDestination(resident.hobWay1);
                    GameManager.prosperity--;
                }
            }
        }
    }
}
