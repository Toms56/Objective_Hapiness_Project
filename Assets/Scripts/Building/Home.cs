using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Building
{
    // Start is called before the first frame update

    public static int nbrplace = 4;
    void Start()
    {
        StartCoroutine(Construct(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
