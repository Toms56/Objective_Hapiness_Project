﻿
public class Home : Building
{
    public int nbrplace = 4;
    private bool homegood;

    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 1;
        StartCoroutine(Construct(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (builded && !homegood)
        {
            homegood = true;
            GameManager.Instance.homes.Add(gameObject);
            GameManager.Instance.RebuildSurface();
            this.tag = GameManager.Buildings.Home.ToString();
        }

        if (GameManager.day)
        {
            nbrplace = 4;
        }
    }
}
