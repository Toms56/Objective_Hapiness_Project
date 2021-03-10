using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] GameObject hobo;


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
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0 :
                    GameObject Builder = Instantiate(hobo, transform);
                    Builder.name = "Builder";
                    Builder.tag = "Builder";
                    Destroy(Builder.GetComponent<H_Resident>());
                    Builder.AddComponent<Builder>();
                    Builder.SetActive(true);
                    break;
                case 1 : 
                    GameObject Harvester = Instantiate(hobo, transform);
                    Harvester.name = "Harvester";
                    Harvester.tag = "Harvester";
                    Destroy(Harvester.GetComponent<H_Resident>());
                    Harvester.AddComponent<Harvester>();
                    Harvester.SetActive(true);
                    break;
                case 2 :
                    GameObject Lumberjack = Instantiate(hobo, transform);
                    Lumberjack.name = "Lumberjack";
                    Lumberjack.tag = "Lumberjack";
                    Destroy(Lumberjack.GetComponent<H_Resident>());
                    Lumberjack.AddComponent<Lumberjack>();
                    Lumberjack.SetActive(true);
                    break;
                case 3 :
                    GameObject Minor = Instantiate(hobo, transform);
                    Minor.name = "Minor";
                    Minor.tag = "Minor";
                    Destroy(Minor.GetComponent<H_Resident>());
                    Minor.AddComponent<Minor>();
                    Minor.SetActive(true);
                    break;
            }
        }
    }
}
