using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [SerializeField] private Camera cam;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region SelectResident
        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0f;
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D[] arraycast = Physics2D.RaycastAll(mousepos, Vector3.forward, 10f);
                /*if (arraycast.Length != 0)
                {
                    for (int i = 0; i < arraycast.Length; i++)
                    {
                        RaycastHit2D element = arraycast[i];
                        if (element.collider != null && element.collider.CompareTag("camp") &&
                            !element.collider.CompareTag("tower"))
                        {
                            followtower.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.8f);
                            if (Input.GetButtonDown("Fire2"))
                            {
                                element.collider.tag = "tower";
                                followtower.GetComponent<Tower>().builded = true;
                                building = false;
                            }
                        }
                        else
                        {
                            followtower.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.8f);
                        }
                    }
                }
                else
                {
                    if (followtower != null)
                    {
                        followtower.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.8f);
                    }
                } */
        }
        #endregion
    }
}
