
public class School : Building
{
    private bool good;

    void Start()
    {
        buildersNeed = 1;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (builded)
        {
            GameManager.Instance.school = gameObject;
            GameManager.schoolBuilded = true;
            this.tag = GameManager.Buildings.School.ToString();
        }
    }
}
