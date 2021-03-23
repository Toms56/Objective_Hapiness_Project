using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool construction = false;
    public float interpol = 0;
    protected bool builded = false;
    public SpriteRenderer spriteRend;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(Construct());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Builded : " + construction);
    }

    public  IEnumerator Construct()
    {
        yield return new WaitUntil(() => construction);
        spriteRend.color = Color.clear;     
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(Color.clear, Color.magenta, interpol);
            interpol += 0.5f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        builded = true;
    }
}
