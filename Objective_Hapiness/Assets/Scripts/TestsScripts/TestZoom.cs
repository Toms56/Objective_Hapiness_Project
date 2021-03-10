using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZoom : MonoBehaviour
{
    public static TestZoom Instance;

    private void Awake()
    {
        // Création de l'instance du GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetScreenBounds()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
}
