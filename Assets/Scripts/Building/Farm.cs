
public class Farm : Building
{
    private bool farmGood;

    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded && !farmGood)
        {
            farmGood = true;
            GameManager.nbrFarm++;
            this.tag = GameManager.Buildings.Farm.ToString();
        }
    }
}
