using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Construct(0.5f));
        GameManager.Instance.nbrFarm++;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.Instance.nbrFarm);
    }
}
