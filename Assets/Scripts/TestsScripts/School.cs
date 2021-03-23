using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : Building
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (builded == true)
        {
            GameManager.Instance.school = this.gameObject;
        }
    }
}
