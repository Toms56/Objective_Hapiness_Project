using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum : Building
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Construct(0.5f));
        GameManager.Instance.prosperity += 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
