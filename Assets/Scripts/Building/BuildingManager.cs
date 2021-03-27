using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] GameObject[] buildingPrefab = new GameObject [5];
    private GameObject thisBuilding;
    [SerializeField] float mindistance = 50f;
    [SerializeField] Color dontbuild;
    [SerializeField] Color build;
    [SerializeField] LayerMask _mask;

    [SerializeField] Camera _camera;
    private Vector3 mousepos;

    private bool spawned;

    private int builderIndex;

    public static Dictionary<Vector3, int> dictoConstructions = new Dictionary<Vector3, int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousepos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 1;

        if (spawned)
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
                            GameManager.stone -= 5;
                            GameManager.food -= 1;
                            thisBuilding.GetComponent<Home>().construction = true;
                            break;

                        case 1:
                            spawned = false;
                            GameManager.stone -= 6;
                            GameManager.food -= 7;
                            thisBuilding.GetComponent<School>().construction = true;
                            break;

                        case 2:
                            spawned = false;
                            GameManager.stone -= 6;
                            GameManager.food -= 7;
                            thisBuilding.GetComponent<Farm>().construction = true;
                            break;

                        case 3:
                            spawned = false;
                            GameManager.stone -= 6;
                            GameManager.food -= 7;
                            thisBuilding.GetComponent<Librairy>().construction = true;
                            break;

                        case 4:
                            spawned = false;
                            GameManager.stone -= 6;
                            GameManager.food -= 7;
                            thisBuilding.GetComponent<Museum>().construction = true;
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
                    if (GameManager.stone >= 5 && GameManager.wood >= 1 && GameManager.nbrBuilder >= 1)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity,this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;
                case 1:
                    if (GameManager.stone >= 6 && GameManager.wood >= 7 && GameManager.nbrBuilder >= 1)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity,this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;
                case 2:
                    if (GameManager.stone >= 6 && GameManager.wood >= 7 && GameManager.nbrBuilder >= 4)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity, this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;
                case 3:
                    if (GameManager.stone >= 6 && GameManager.wood >= 7 && GameManager.nbrBuilder >= 4)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity, this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;
                case 4:
                    if (GameManager.stone >= 6 && GameManager.wood >= 7 && GameManager.nbrBuilder >= 4)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity, this.gameObject.transform); }
                    else
                    { UI_Manager.Instance.NotEnoughRessources(); }
                    break;
            }
        }
    }

    bool CircleTest()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(thisBuilding.transform.position, mindistance, Vector2.zero, _mask);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null /*&& hit[i].collider.gameObject != thisBuilding*/)
            {
                //Debug.Log(hit[i].collider.gameObject);
                return false; 
            } 
        }
        return true;
    }
}
