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
        for (int i = 0; i < 50; i++)
        {
            Instantiate(hobo, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
