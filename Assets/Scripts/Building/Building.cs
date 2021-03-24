using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public bool construction = false;
    public float interpol = 0;
    protected bool builded = false;
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
