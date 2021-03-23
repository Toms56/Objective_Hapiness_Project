﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables

    private int turn = 0;
    private GameObject resident;
    //CountDown TimeManagement
    private float startingTime;
    [SerializeField]private float totalTime;
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
    public GameObject panelSelectedPnj;
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

    //public static event OnTimeAdvanceHandler OnTimeAdvance;
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
        prosperityBar.value = GameManager.Instance.prosperity;
        #endregion
        
        #region timerManagement
        advanceTimer = turnDuration;
        #endregion

        /*#region CountDownJob
        startingTime = totalTime;
        #endregion*/

        bool changeWork = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

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
        foodText.text = " " + GameManager.Instance.food;
        woodText.text = " " + GameManager.Instance.wood;
        stoneText.text = " " + GameManager.Instance.stone;
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
            if (totalTime <= 0)
            {
                useEventTimer = false;
            }
            /*if (minutes <= 0 && seconds <= 0)
            {
                totalTime = startingTime;
                Debug.Log("TimerEvent");
                TimerEvent.Invoke();
                useEventTimer = false;
            }*/
        }
        countDownTxt.text = "Studing time : "+ minutes.ToString() + " : " + seconds.ToString();
        #endregion
        
        SelectResident();
    }

    void ActiveCountDown(int time)
    {
        #region CountDownJob
        //startingTime = totalTime;
        totalTime = time;
        useEventTimer = true;
        #endregion
    }

    public void SelectResident()
    {
        #region SelectResident
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0f;
            RaycastHit2D[] arraycast = Physics2D.RaycastAll(mousepos, Vector3.forward, 10f);
            Debug.DrawLine(mousepos,Vector3.forward,Color.red);
            if (arraycast.Length != 0)
            {
                for (int i = 0; i < arraycast.Length; i++)
                {
                    Debug.Log(arraycast[i].collider.gameObject);
                    RaycastHit2D element = arraycast[i];
                    if (element.collider != null && element.collider.CompareTag("Hobo"))
                    {
                        print("hobo hit");
                        panelSelectedPnj.SetActive(true);
                        ageText.text = "Age : " + GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + GetComponent<H_Resident>().name;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Builder"))
                    {
                        print("Builder Hit");
                        panelSelectedPnj.SetActive(true);
                        ageText.text = "Age : " + GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + GetComponent<H_Resident>().name;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Lumberjack"))
                    {
                        print("Lumberjack hit");
                        panelSelectedPnj.SetActive(true);
                        ageText.text = "Age : " + GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + GetComponent<H_Resident>().name;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Harvester"))
                    {
                        print("Harvester hit");
                        panelSelectedPnj.SetActive(true);
                        ageText.text = "Age : " + GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + GetComponent<H_Resident>().name;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Minor"))
                    {
                        print("Minor hit");
                        panelSelectedPnj.SetActive(true);
                        ageText.text = "Age : " + GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + GetComponent<H_Resident>().name;
                    }
                }
            }
        }
        #endregion
    }

    public void OnClickBecomeBuilder()
    {
        panelJobSelection.SetActive(false);
        totalTime = 5;
        useEventTimer = true;
        Debug.Log("He's learning a new job");
        if (totalTime <= 0)
        {
            GameManager.Instance.ChangeWork(resident, GameManager.Works.Builder);
            Debug.Log("His job has changed");
        }
    }
    public void OnClickBecomeHarvester()
    {
        panelJobSelection.SetActive(false);
        //totalTime = 5;
        useEventTimer = true;
        Debug.Log("He's learning a new job");
        if (totalTime <= 0)
        {
            GameManager.Instance.ChangeWork(resident, GameManager.Works.Harvester);
            Debug.Log("His job has changed");
            //totalTime = 5;
        }
    }
    public void OnClickBecomeLumberjack()
    {
        panelJobSelection.SetActive(false);
        ActiveCountDown(6);
        Debug.Log("He's learning a new job");
        if (totalTime <= 0)
        {
            useEventTimer = false;
            GameManager.Instance.ChangeWork(resident, GameManager.Works.Lumberjack);
            Debug.Log("His job has changed");
            //totalTime = 5;
        }
    }
    public void OnClickBecomeMinor()
    {
        panelJobSelection.SetActive(false);
        totalTime = 5;
        useEventTimer = true;
        Debug.Log("He's learning a new job");
        if (totalTime <= 0)
        {
            GameManager.Instance.ChangeWork(resident, GameManager.Works.Minor);
            Debug.Log("His job has changed");
        }
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

    public void OnClickClosePnjPanel()
    {
        panelSelectedPnj.SetActive(false);
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
