using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefab = new GameObject [5];
    private GameObject thisBuilding;
    [SerializeField] float mindistance = 50f;
    [SerializeField] private Transform buildingTransform;
    [SerializeField] private Color dontbuild;
    [SerializeField] private Color build;

    [SerializeField] private Camera _camera;
    private Vector3 mousepos;

    private bool spawned = false;

    [SerializeField] LayerMask _mask;

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
            Debug.Log(" CircleTest" + CircleTest());
           //Debug.DrawRay(thisBuilding.transform.position, transform.up * mindistance, Color.red);
            thisBuilding.transform.position = mousepos;
            //Checkbuildcamp();
            if (CircleTest())
            {
                thisBuilding.GetComponent<SpriteRenderer>().color = build;
                if (Input.GetButtonDown("Fire1"))
                {
                    spawned = false;
                }
            } 
            else
            {
                thisBuilding.GetComponent<SpriteRenderer>().color = dontbuild;
            }
        }
    }

    public void onClickInstBuild(int index)
    {
        spawned = true; ;
        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity);
    }

    bool CircleTest()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(thisBuilding.transform.position, mindistance, Vector2.zero, _mask);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.gameObject != thisBuilding)
            {
                Debug.Log(hit[i].collider.gameObject);
                return false;
            }
        }
        return true;
    }

    /* bool CircleTest()
     {
         RaycastHit2D hit = Physics2D.CircleCast(thisBuilding.transform.position, mindistance, Vector2.up, _mask);
         if (hit.collider != null && hit.collider.CompareTag("Building") && hit.collider.gameObject != thisBuilding)
             {
                 return false;
             Debug.DrawLine(thisBuilding.transform.position, hit.point, Color.red, 5f);
             }
         return true;
     } */
}
