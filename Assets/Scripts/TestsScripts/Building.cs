using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool builded = false;
    public float interpol = 0;
    [SerializeField] SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Construct());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Builded : " + builded);
    }

    IEnumerator Construct()
    {
        yield return new WaitUntil(() => builded);
        spriteRend.color = Color.clear;     
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(Color.clear, Color.magenta, interpol);
            interpol += 0.5f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
