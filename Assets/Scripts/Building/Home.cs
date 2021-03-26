

using UnityEngine;

public class Home : Building
{
    public int nbrplace = 4;
    private bool homegood;

    [SerializeField] private BoxCollider2D collider2d;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Construct(0.5f));
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
    }
}
