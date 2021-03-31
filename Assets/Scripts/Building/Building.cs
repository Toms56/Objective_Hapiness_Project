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
    
    // Coroutine for the construction : 
        // Wait until the construct variable to become true
            // As long as the alpha of the color is lower than 1
                // Lerp to set alpha 1 / Activate the building Nav Mesh Obstacle
        // At the end the variable construct become false and the variable builded become true
    public IEnumerator Construct(float addInterpol)
    {
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(new Color(0,0,0,0.2f), Color.white, interpol);
            interpol += addInterpol * Time.deltaTime;
            navObstacle.enabled = true;
            yield return new WaitForUpdate();
        }
        construction = false;
        builded = true;
    }

    // Activate the coroutine when a builder trigger with the building
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.Works.Builder.ToString()))
        {
            StartCoroutine(Construct(0.15f));
        }
    }

    // Adding and deleting buildings in the list according to certain conditions
    protected virtual void Update()
    {
        if (construction && !BuildingManager.dictoConstructions.ContainsKey(gameObject.transform.position))
        {
            BuildingManager.dictoConstructions.Add(gameObject.transform.position, buildersNeed);
            col2d.enabled = true; // Activate the collider of the building
            //Makes the number of builders necessary for the construction unfit 
            GameManager.nbrBuilder -= buildersNeed;
        }

        if (builded && !navGood)
        {
            navGood = true; // To avoid doing several times the actions of the condition
            GameManager.Instance.RebuildSurface(); // Rebuild the suiface of the NavMesh 
            if (BuildingManager.dictoConstructions.ContainsKey(gameObject.transform.position))
            {
                BuildingManager.dictoConstructions.Remove(gameObject.transform.position);
            }
        }
    }
}
