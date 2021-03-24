using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : Building
{
    private void Start()
    {
        StartCoroutine(Construct(0.5f));
    }
    // Update is called once per frame
    void Update()
    {
        if (builded == true)
        {
            GameManager.Instance.school = this.gameObject;
            GameManager.Instance.RebuildSurface();
        }
    }
}
