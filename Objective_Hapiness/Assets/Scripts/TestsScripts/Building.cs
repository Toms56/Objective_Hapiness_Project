using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    private GameObject thisbuilding;
    float mindistance = 5;
    [SerializeField] private Transform buildingTransform;
    [SerializeField] private Color dontbuild;
    [SerializeField] private Color build;

    [SerializeField] private Camera _camera;
    private Vector3 mousepos;

    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 1;
        if (spawned == true)
        {
            Debug.Log(CircleTest());
            thisbuilding.transform.position = mousepos;
            Checkbuildcamp();
           /* if (Input.GetButtonDown("Fire1"))
            {
                
            } */
        }
    }

    public void onClickInstBuild()
    {
        spawned = true; ;
        Debug.Log("Test");
        thisbuilding = Instantiate(buildingPrefab, transform.position, Quaternion.identity);
    }

    void Checkbuildcamp()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(thisbuilding.transform.position, Vector3.up, 10f);
        Debug.Log("hits Raycast Lenght : " + hits.Length);
        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                thisbuilding.GetComponent<SpriteRenderer>().color = build;
                if (CircleTest() && Input.GetButtonDown("Fire1"))
                {
                    spawned = false;
                    thisbuilding.transform.position = hits[i].transform.position;
                }
                else
                {
                    thisbuilding.GetComponent<SpriteRenderer>().color = dontbuild;
                }
            }
        }
        else
        {
            thisbuilding.GetComponent<SpriteRenderer>().color = dontbuild;
        }
    }

    bool CircleTest()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(thisbuilding.transform.position, mindistance, Vector2.up);

        if (hit.Length == 0) return true;
        Debug.Log("hit Lenth : " + hit.Length);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject != thisbuilding && hit[i].collider.CompareTag("Building"))
            {
                return false;
            }
        }
        return true;
    }
}
