using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public bool construction;
    public float interpol;
    protected bool builded;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] NavMeshObstacle navObstacle;

    public  IEnumerator Construct(float addInterpol)
    {
        yield return new WaitUntil(() => construction);
        spriteRend.color = Color.clear;     
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(Color.clear, Color.magenta, interpol);
            interpol += addInterpol * Time.deltaTime;
            navObstacle.enabled = true;
            yield return new WaitForFixedUpdate();
        }
        builded = true;
    }
}
