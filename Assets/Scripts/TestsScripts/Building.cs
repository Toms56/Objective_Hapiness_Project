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
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(new Vector4(1f, 1f, 1f, 0f), new Vector4(0.5f, 0.5f, 0.5f, 1f), interpol);
            interpol += 0.5f * Time.deltaTime;
            //yield return new;
        }
    }
}
