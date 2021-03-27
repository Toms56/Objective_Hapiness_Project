
public class School : Building
{
    private bool good;

    private void Start()
    {
        buildersNeed = 1;
        StartCoroutine(Construct(0.1f));
    }
    // Update is called once per frame
    void Update()
    {
        if (builded && ! good)
        {
            good = true;
            GameManager.Instance.school = gameObject;
            GameManager.Instance.schoolBuilded = true;
            GameManager.Instance.RebuildSurface();
            this.tag = GameManager.Buildings.School.ToString();
        }
    }
}
