using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables
    
    //CountDown TimeManagement
    private float startingTime;
    public float totalTime;
    public Text countDownTxt;

    private float minutes;
    private float seconds;

    public bool useEventTimer = false;
    public UnityEvent TimerEvent;
    
    //UI Management
    //public GameObject panelResources;
    //public GameObject panelResident;
    public GameObject panelJobSelection;
    public GameObject panelBuildingSelection;
    public Text foodText;
    public Text woodText;
    public Text stoneText;
    
    public Text ageText;
    public Text jobText;

    private GameObject selectedResident;
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

        #region CountDownJob
        startingTime = totalTime;
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

        #region RessourcesManagement
        /*foodText.text = "Food : " + GameManager.Instance.food;
        woodText.text = "Wood : " + GameManager.Instance.wood;
        stoneText.text = "Stone : " + GameManager.Instance.stone;*/
        #endregion
        
        #region TextDisplay

        if (gameObject != null)
        {
            /*ageText.text = "Score : " + selectedResident.GetComponent<H_Resident>().age;
            jobText.text = "Job : " + gameObject.name;*/
        }

        #endregion

        #region JobCountDownDisplay

        minutes = (int) (totalTime / 60);
        seconds = (int) (totalTime % 60);

        if (useEventTimer)
        {
            totalTime -= Time.deltaTime;
            if (minutes <= 0 && seconds <= 0)
            {
                totalTime = startingTime;
                Debug.Log("TimerEvent");
                TimerEvent.Invoke();
            }
        }
        countDownTxt.text = "Studing time : "+ minutes.ToString() + " : " + seconds.ToString();
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

    public void OnClickDisplayChangeJob()
    {
        panelJobSelection.SetActive(true);
    }

    public void OnClickCloseJobPanel()
    {
        panelJobSelection.SetActive(false);
    }

    public void OnClickDisplayBuilding()
    {
        panelBuildingSelection.SetActive(true);
    }

    public void OnClickCloseBuildingPanel()
    {
        panelBuildingSelection.SetActive(false);
    }
}
