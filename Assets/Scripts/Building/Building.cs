using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public bool construction;
    public float interpol;
    protected bool builded;
    public SpriteRenderer spriteRend;
    [SerializeField] NavMeshObstacle navObstacle;
    public Collider2D col2d;
    public int buildersNeed;
    public bool navGood;

    public IEnumerator Construct(float addInterpol)
    {
        yield return new WaitUntil(() => construction);
        spriteRend.color = Color.clear;
        while (spriteRend.color.a < 1)
        {
            Debug.Log("Dedans");
            spriteRend.color = Vector4.Lerp(Color.clear, new Vector4(1,1,1,1), interpol);
            interpol += addInterpol * Time.deltaTime;
            navObstacle.enabled = true;
            yield return new WaitForFixedUpdate();
            builded = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.Works.Builder.ToString()))
        {
            StartCoroutine(Construct(0.1f));
        }
    }

   protected virtual void Update()
    {
        if (construction == true && !BuildingManager.dictoConstructions.ContainsKey(gameObject.transform.position))
        {
            BuildingManager.dictoConstructions.Add(gameObject.transform.position, buildersNeed);
            col2d.enabled = true;
        }

        if (builded && !navGood)
        {
            navGood = true;
            GameManager.Instance.RebuildSurface();
            BuildingManager.dictoConstructions.Remove(gameObject.transform.position);
        }
    }
}
