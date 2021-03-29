
public class Librairy : Building
{    
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 4;
        GameManager.prosperity += 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded)
        {
            this.tag = GameManager.Buildings.Librairy.ToString();
        }
    }
}
