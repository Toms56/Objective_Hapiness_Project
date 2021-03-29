using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Variables
    public static PoolManager Instance;

    [SerializeField] GameObject hobo;
    [SerializeField] GameObject builder;
    [SerializeField] GameObject harvester;
    [SerializeField] GameObject lumberjack;
    [SerializeField] GameObject minor;

    public static Queue<GameObject> inactiveResidents = new Queue<GameObject>();
    public static List<GameObject> activeResidents = new List<GameObject>();

    private bool foodverification;
    private int foodavailable;
    private bool stopspawn;
    #endregion

    private void Awake()
    { if (Instance == null)
        { Instance = this; }
        else
        { Destroy(this.gameObject); } 
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnResidents();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.day && !foodverification)
        {
            foodverification = true;
            Foodverification();
            SpawnHobo();
        }
        if (GameManager.day)
        {
            foodverification = false;   
        }
        //limits the total number of residents to 100.
        if (inactiveResidents.Count + activeResidents.Count >= 100 && !stopspawn)
        {
            stopspawn = true;
        }
    }
    //checks if there is enough food for each resident knowing that a resident consumes one food
    //at the end of each day. if food is lacking, they randomly "die".
    private void Foodverification()
    {
        foodavailable = GameManager.food - activeResidents.Count;
        if (foodavailable >= 0)
        {
            GameManager.food = foodavailable;
            UI_Manager.Instance.EnoughFood();
        }
        else
        {
            if (activeResidents.Count == 0)
            {
                GameManager.gameOver = true;
            }
            else
            {
                int killresidents = Mathf.Abs(foodavailable);
                for (int i = 0; i < killresidents; i++)
                {
                    int supp = Random.Range(0,activeResidents.Count);
                    activeResidents[supp].GetComponent<H_Resident>().ResetToHobo();
                    inactiveResidents.Enqueue(activeResidents[supp]);
                    activeResidents[supp].gameObject.SetActive(false);
                    activeResidents.RemoveAt(supp);
                }
                GameManager.prosperity -= killresidents * 2;
                UI_Manager.Instance.ShowKillResidents(killresidents);
                
                GameManager.food = foodavailable;
            }
        }
    }
    //spawns a resident of each profession as well as a hobo at the start of the game.
    private void SpawnResidents()
    {
        for (int i = 0; i < 10; i++)
        {
            inactiveResidents.Enqueue(Instantiate(hobo, transform));
        }

        for (int i = 0; i < 2; i++)
        {
            activeResidents.Add(Instantiate(builder, transform));
        }

        activeResidents.Add(Instantiate(harvester, transform));
        activeResidents.Add(Instantiate(lumberjack, transform));
        activeResidents.Add(Instantiate(minor, transform));

        SpawnHobo();
    }
    //activates a hobo at the end of each day and respawns 5 if it runs out.
    private void SpawnHobo()
    {
        if (inactiveResidents.Count <= 2 && !stopspawn)
        {
            for (int i = 0; i < 5; i++)
            {
                inactiveResidents.Enqueue(Instantiate(hobo, transform));
            }
        } 
        GameObject activeHobo = inactiveResidents.Dequeue(); 
        activeHobo.SetActive(true); 
        activeResidents.Add(activeHobo);
    }
}
