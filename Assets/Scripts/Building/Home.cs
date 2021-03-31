
public class Home : Building
{
    public int nbrplace = 4;
    private bool homegood;

    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded && !homegood)
        {
            homegood = true;
            GameManager.Instance.homes.Add(gameObject);
            this.tag = GameManager.Buildings.Home.ToString();
        }

        if (GameManager.day)
        {
            nbrplace = 4;
        }
    }
}
