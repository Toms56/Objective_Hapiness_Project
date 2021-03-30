
public class Museum : Building
{    
    private bool addprosperity;
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 8;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded && !addprosperity)
        {
            addprosperity = true;
            GameManager.prosperity += 10;
            this.tag = GameManager.Buildings.Museum.ToString();
        }
    }
}
