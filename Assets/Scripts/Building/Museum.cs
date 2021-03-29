
public class Museum : Building
{    
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 8;
        GameManager.prosperity += 10;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded)
        {
            this.tag = GameManager.Buildings.Museum.ToString();
        }
    }
}
