using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] GameObject[] buildingPrefab = new GameObject [5];
    private GameObject thisBuilding;
    [SerializeField] float mindistance;
    [SerializeField] Color dontbuild;
    [SerializeField] Color build;
    [SerializeField] LayerMask _mask;

    [SerializeField] Camera _camera;
    private Vector3 mousepos;

    private bool spawned;

    private int builderIndex;

    public static Dictionary<Vector3, int> dictoConstructions = new Dictionary<Vector3, int>();

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

            // If the circle test is true use the fonction Construction
            if (CircleTest())
            {
                Construction();
            } 
            // Else the color sprite is red
            else
            {
                thisBuilding.GetComponent<SpriteRenderer>().color = dontbuild;
            }
            
            // You can cancel the building in your hand with the right clic
            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(thisBuilding.gameObject);
                spawned = false;
            }
        }
    }

    private void Construction()
    {
        thisBuilding.GetComponent<SpriteRenderer>().color = build;
        if (Input.GetButtonDown("Fire1"))
        {
            // Switch with the builder Index to combine the different components with the right beat
            switch (builderIndex)
            {
                case 0 :
                    // Set the spawned variable to false;
                    spawned = false;
                    //Remove the necessary number of resources from the BuildingManager
                    GameManager.stone -= 5;
                    GameManager.wood -= 1;

                    //Set the construction variable of the building to true in order to allow the addition to the list of constructions and change his color
                    thisBuilding.GetComponent<Home>().construction = true;
                    thisBuilding.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    break;

                case 1:
                    spawned = false;
                    GameManager.stone -= 6;
                    GameManager.wood -= 7;
                    thisBuilding.GetComponent<School>().construction = true;
                    thisBuilding.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    break;

                case 2:
                    spawned = false;
                    GameManager.stone -= 6;
                    GameManager.wood -= 7;
                    thisBuilding.GetComponent<Farm>().construction = true;
                    thisBuilding.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    break;

                case 3:
                    spawned = false;
                    GameManager.stone -= 6;
                    GameManager.wood -= 7;
                    thisBuilding.GetComponent<Librairy>().construction = true;
                    thisBuilding.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    break;

                case 4:
                    spawned = false;
                    GameManager.stone -= 6;
                    GameManager.wood -= 7;
                    thisBuilding.GetComponent<Museum>().construction = true;
                    thisBuilding.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    break;
            }
        }
    }

    // Fonction for the buttion
    public void onClickInstBuild(int index)
    {
        builderIndex = index;
        if (!spawned)
        {
            switch (index)
            {
                case 0: 
                    // Condition for check if the player have the necessary ressources for construct the building
                    if (GameManager.stone >= 5 && GameManager.wood >= 5 && GameManager.nbrBuilder >= 1)
                    {
                        // Set the variable spawned to true and instanciate the prefab of batiment and puts it as a child of the BuildingManager
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity,this.gameObject.transform);
                    }

                    // Else activate the function "NotEnoughRessources" of the UI-Manager class
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;

                case 1:
                    if (GameManager.stone >= 4 && GameManager.wood >= 5 && GameManager.nbrBuilder >= 1)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity,this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;

                case 2:
                    if (GameManager.stone >= 6 && GameManager.wood >= 9 && GameManager.nbrBuilder >= 3)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity, this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;

                case 3:
                    if (GameManager.stone >= 10 && GameManager.wood >= 12 && GameManager.nbrBuilder >= 4)
                    {
                        spawned = true;
                        thisBuilding = Instantiate(buildingPrefab[index], transform.position, Quaternion.identity, this.gameObject.transform);
                    }
                    else
                    {
                        UI_Manager.Instance.NotEnoughRessources(); }
                    break;

                case 4:
                    if (GameManager.stone >= 6 && GameManager.wood >= 7 && GameManager.nbrBuilder >= 6)
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
        // Raycast for detect the collision with other object
        RaycastHit2D[] hit = Physics2D.CircleCastAll(thisBuilding.transform.position, mindistance, Vector2.zero, _mask);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                return false; 
            } 
        }
        return true;
    }
}
