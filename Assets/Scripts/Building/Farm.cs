
public class Farm : Building
{
    private bool good;

    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 4;
        StartCoroutine(Construct(0.1f));
        GameManager.nbrFarm++;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (builded && !good)
        {
            good = true;
            GameManager.Instance.RebuildSurface();
            this.tag = GameManager.Buildings.Farm.ToString();
        }
    }
}
