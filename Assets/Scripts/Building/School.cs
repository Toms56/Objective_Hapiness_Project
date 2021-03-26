
public class School : Building
{
    private bool good;

    private void Start()
    {
        StartCoroutine(Construct(0.5f));
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
        }
    }
}
