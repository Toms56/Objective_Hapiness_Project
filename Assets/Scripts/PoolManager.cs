using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] GameObject hobo;
    [SerializeField] GameObject builder;
    [SerializeField] GameObject harvester;
    [SerializeField] GameObject lumberjack;
    [SerializeField] GameObject minor;

    public Queue<GameObject> inactiveResidents = new Queue<GameObject>();
    public List<GameObject> activeResidents = new List<GameObject>();

    private bool foodverification;
    private int foodavailable;

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
        if (GameManager.Instance.endofday && !foodverification)
        {
            foodverification = true;
            Foodverification();
            SpawnHobo();
        }
        if (!GameManager.Instance.endofday)
        {
            foodverification = false;   
        }
    }

    private void Foodverification()
    {
        foodavailable = GameManager.food - activeResidents.Count;
        Debug.Log("residents :" + activeResidents.Count);
        Debug.Log("food available : " + foodavailable);
        if (foodavailable > 0)
        {
            GameManager.food = foodavailable;
        }
        else
        {
            if (activeResidents.Count == 0)
            {
                Debug.Log("Game Over");
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
                GameManager.food = foodavailable;
                Debug.Log("residents killed : " + killresidents);
            }
        }
    }
    
    private void SpawnResidents()
    {
        for (int i = 0; i < 10; i++)
        {
            inactiveResidents.Enqueue(Instantiate(hobo, transform));
        }
        activeResidents.Add(Instantiate(builder, transform));
        activeResidents.Add(Instantiate(harvester, transform));
        activeResidents.Add(Instantiate(lumberjack, transform));
        activeResidents.Add(Instantiate(minor, transform));

        GameObject hoboFirst = Instantiate(hobo, transform);
        hoboFirst.SetActive(true);
        activeResidents.Add(hoboFirst);
    }

    private void SpawnHobo()
    {
        if (inactiveResidents.Count <= 2)
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
