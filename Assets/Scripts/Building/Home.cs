using UnityEngine;

public class Home : Building
{
    public int nbrplace = 4;
    private bool homegood;

    [SerializeField] private BoxCollider2D collider2d;
    // Start is called before the first frame update
    void Start()
    {
        buildersNeed = 4;
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
            collider2d.enabled = true;
        }

        if (GameManager.Instance.day)
        {
            nbrplace = 4;
        }
    }
}
