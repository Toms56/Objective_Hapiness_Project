using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables

    public static UI_Manager Instance;

    [SerializeField] private Camera cam;

    //TimeScale management
    public float turnDuration = 1f;
    public float fastForwardMultiplier = 5;
    public bool pause = false;
    public bool fastForward;
    private float advanceTimer;
    public delegate void OnTimeAdvanceHandler();

    public static event OnTimeAdvanceHandler OnTimeAdvance;
    
    //ProsperityBar Management
    public Slider prosperityBar;
    #endregion
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
        #region proserity

        

        #endregion
        prosperityBar.value = GameManager.Instance.prosperity;

        #region timerManagement
        advanceTimer = turnDuration;
        #endregion
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
            if (arraycast.Length != 0)
            {
                for (int i = 0; i < arraycast.Length; i++)
                {
                    RaycastHit2D element = arraycast[i];
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

        #region TimeMAnagement

        if (!pause)
        {
            advanceTimer -= Time.deltaTime * (fastForward ? fastForwardMultiplier : 1f);
            if (advanceTimer <= 0)
            {
                advanceTimer += turnDuration;
                OnTimeAdvance?.Invoke(); //the Event will be Invoked only if it's not null
            }
        }
        #endregion
    }

    public void Step()
    {
       OnTimeAdvance?.Invoke(); 
    }
    public void Pause()
    {
        pause = true;
        fastForward = false;
        /*if (pause == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        
        if (pause == true)
        {
            pause = false;
            Debug.Log("Game on Pause : " + pause);
        }
        else
        {
            pause = true;
            Debug.Log("The Game has resumed : " + pause);
        }*/
    }

    public void Play()
    {
        pause = false;
        fastForward = false;
    }
    public void FastForward()
    {
        pause = false;
        fastForward = true;
    }
}
