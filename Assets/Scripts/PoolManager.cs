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

    private List<GameObject> residents = new List<GameObject>();
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
        StartCoroutine(changework());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.endofday && !foodverification)
        {
            foodverification = true;
            Foodverification();
        }

        if (!GameManager.Instance.endofday)
        {
            foodverification = false;
        }
    }

    private void Foodverification()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                residents.Add(transform.GetChild(i).gameObject);
            }
        }
        foodavailable = GameManager.Instance.food - residents.Count;
        Debug.Log("residents :" + residents.Count);
        Debug.Log("food available : " + foodavailable);
        if (foodavailable > 0)
        {
            GameManager.Instance.food = foodavailable;
        }
        else
        {
            if (residents.Count == 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                int killresidents = Mathf.Abs(foodavailable);
                for (int i = 0; i < killresidents; i++)
                {
                    residents[Random.Range(0,residents.Count)].gameObject.SetActive(false);
                    Debug.Log("resident kill");
                }
                GameManager.Instance.food = foodavailable;
                Debug.Log("residents killed : " + killresidents);
            }
        }
        residents.Clear();
    }
    
    private void SpawnResidents()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(hobo, transform);
        }

        Instantiate(builder, transform);
        Instantiate(harvester, transform);
        Instantiate(lumberjack, transform);
        Instantiate(minor, transform);
    }

    IEnumerator changework()
    {
        GameObject hobo1 = Instantiate(hobo, transform);
        hobo1.SetActive(true);
        yield return new WaitForSeconds(10);
        GameManager.Instance.ChangeWork(hobo1,GameManager.Works.Builder);
    }
}
