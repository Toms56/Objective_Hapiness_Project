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
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0f;
            RaycastHit2D[] arraycast = Physics2D.RaycastAll(mousepos, Vector3.forward, 10f);
            print(arraycast.Length);
            if (arraycast.Length != 0)
            {
                for (int i = 0; i < arraycast.Length; i++)
                {
                    RaycastHit2D element = arraycast[i];
                    print(element);
                    if (element.collider != null && element.collider.CompareTag("Hobo"))
                    {
                        print("hobo hit");
                    }
                    else
                    {
                        print("not hobo hit");
                    }
                }
            }
            print("not raycast");
        }
        #endregion
    }


    public void OnclickResident()
    {
        print("hobo hit");
        Debug.Log("hobo hitttttt");
    }
}
