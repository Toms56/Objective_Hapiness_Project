
public class Librairy : Building
{
    private bool good;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Construct(0.5f));
        GameManager.prosperity += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (builded && !good)
        {
            good = true;
            GameManager.Instance.RebuildSurface();
        }
    }
}
