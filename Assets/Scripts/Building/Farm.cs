
public class Farm : Building
{
    private bool good;

    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 4;
        GameManager.nbrFarm++;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded)
        {
            this.tag = GameManager.Buildings.Farm.ToString();
        }
    }
}
