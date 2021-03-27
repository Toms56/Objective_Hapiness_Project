
public class Museum : Building
{
    private bool good;
    
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 8;
        StartCoroutine(Construct(0.1f));
        GameManager.prosperity += 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (builded && !good)
        {
            good = true;
            GameManager.Instance.RebuildSurface();
            this.tag = GameManager.Buildings.Museum.ToString();
        }
    }
}
