using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] GameObject hobo;
    [SerializeField] GameObject builder;
    [SerializeField] GameObject harvester;
    [SerializeField] GameObject lumberjack;
    [SerializeField] GameObject minor;



    private void Awake()
    { if (Instance == null)
        { Instance = this; }
        else
        { Destroy(this.gameObject); } }

    // Start is called before the first frame update
    void Start()
    {
        SpawnResidents();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject hobo1 = Instantiate(hobo, transform);
        hobo1.SetActive(true);
    }

    
}
