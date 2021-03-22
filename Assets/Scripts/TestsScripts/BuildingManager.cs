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
    [SerializeField] LayerMask _mask;

    [SerializeField] private Camera _camera;
    private Vector3 mousepos;

    private bool spawned = false;

    public int builderIndex;

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
            //Debug.Log("CircleTest" + CircleTest());
            //Debug.DrawRay(thisBuilding.transform.position, transform.up * mindistance, Color.red);
            thisBuilding.transform.position = mousepos;
            if (CircleTest())
            {
                thisBuilding.GetComponent<SpriteRenderer>().color = build;
                if (Input.GetButtonDown("Fire1"))
                {
                   
                    switch(builderIndex)
                    {
                        case 0 :
                            spawned = false;
                            GameManager.Instance.stone -= 5;
                            GameManager.Instance.food -= 1;
                            thisBuilding.GetComponent<Building>().builded = true;
                            break;
                        case 1:
                            spawned = false;
                            GameManager.Instance.stone -= 6;
                            GameManager.Instance.food -= 7;
                            thisBuilding.GetComponent<Building>().builded = true;
                            break;
                    }
                }
            } 
            else
            {
                thisBuilding.GetComponent<SpriteRenderer>().color = dontbuild;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(thisBuilding.gameObject);
                spawned = false;
            }
        }
    }

    public void onClickInstBuild(int index)
    {
        builderIndex = index;

        if (!spawned)
        {
            switch (index)
            {
                case 0: 
                    if (GameManager.Instance.stone >= 5 && GameManager.Instance.food >= 1)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Debug.Log("Il vous manque des ressources");
                    }
                    break;

                case 1:
                    if (GameManager.Instance.stone >= 6 && GameManager.Instance.food >= 7)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Debug.Log("Il vous manque des ressources");
                    }
                    break;
            }
           /* spawned = true;
            thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity);*/
        }

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
}
