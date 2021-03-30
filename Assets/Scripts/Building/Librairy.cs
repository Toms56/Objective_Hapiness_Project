
public class Librairy : Building
{
    private bool addprosperity;
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 4;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded && !addprosperity)
        {
            addprosperity = true;
            GameManager.prosperity += 5;
            this.tag = GameManager.Buildings.Librairy.ToString();
        }
    }
}
