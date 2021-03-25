using System;
using System.Collections;
using UnityEngine;
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
    public Text dayCycleText;
    public Text nightCycleText;
    public Text countDownDayCycle;

    private float minutes;
    private float seconds;

    public bool useEventTimer = false;
    public UnityEvent TimerEvent;
    
    //UI Management
    public GameObject panelJobSelection;
    public GameObject panelBuildingSelection;
    public GameObject panelSelectedPnj;
    public GameObject dayCyclePanel;
    public GameObject informationPanel;
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
    private bool play;
    
    //DayTimeManagement
    public float durationDay;
    public float durationNight;
    [SerializeField]private float timeSpent;
    
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
        prosperityBar.value = GameManager.prosperity;
        #endregion
        
        #region timerManagement
        advanceTimer = turnDuration;
        #endregion
        
        DayNightCycle();
    }

    // Update is called once per frame
    void Update()
    {
        #region DayTime

        timeSpent += Time.deltaTime;
        if (timeSpent > durationDay && GameManager.Instance.day)
        {
            GameManager.Instance.day = false;
            timeSpent = 0;
        }else if (timeSpent > durationNight && !GameManager.Instance.day)
        {
            GameManager.Instance.day = true;
            timeSpent = 0;
        }

        if (GameManager.Instance.day)
        {
            countDownDayCycle.text = "DayTime : " + Mathf.RoundToInt(durationDay - timeSpent);   
        }
        if (!GameManager.Instance.day)
        {
            countDownDayCycle.text = "NightTime : " + Mathf.RoundToInt(durationNight - timeSpent);
        }
        
        #endregion
        #region proserity
        prosperityBar.value = GameManager.prosperity;
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
        foodText.text = " " + GameManager.food;
        woodText.text = " " + GameManager.wood;
        stoneText.text = " " + GameManager.stone;
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

    public async void DayNightCycle()
    {
        int dayNum = 0;
        int nightNum = 0;
        while (!play)
        {
            dayCycleText.text = $"Day : {dayNum} ";
            nightCycleText.text = $"Night : {nightNum}";
            if (GameManager.Instance.day)
            {
                await new WaitForSeconds(durationDay);
                GameManager.Instance.day = false;
                dayNum += 1;
                GameManager.prosperity += 3;
            }
            else
            {
                await new WaitForSeconds(durationNight);
                GameManager.Instance.day = true;
                nightNum += 1;
            }
        }
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
                        resident = element.collider.gameObject;
                        ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + resident.GetComponent<H_Resident>().tag;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Builder"))
                    {
                        print("Builder Hit");
                        panelSelectedPnj.SetActive(true);
                        resident = element.collider.gameObject;
                        ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + resident.GetComponent<H_Resident>().tag;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Lumberjack"))
                    {
                        print("Lumberjack hit");
                        panelSelectedPnj.SetActive(true);
                        resident = element.collider.gameObject;
                        ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + resident.GetComponent<H_Resident>().tag;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Harvester"))
                    {
                        print("Harvester hit");
                        panelSelectedPnj.SetActive(true);
                        resident = element.collider.gameObject;
                        ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + resident.GetComponent<H_Resident>().tag;
                    }
                    else if (element.collider != null && element.collider.CompareTag("Minor"))
                    {
                        print("Minor hit");
                        panelSelectedPnj.SetActive(true);
                        resident = element.collider.gameObject;
                        ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                        jobText.text = " Job : " + resident.GetComponent<H_Resident>().tag;
                    }
                }
            }
        }
        #endregion
    }

    public void OnClickBecomeBuilder()
    {
        panelJobSelection.SetActive(false);
        ActiveCountDown(6);
        //Debug.Log("He's learning a new job"+ resident.GetComponent<H_Resident>().tag);
        StartCoroutine(WaitForBecomeBuilder());
    }
    public void OnClickBecomeHarvester()
    {
        panelJobSelection.SetActive(false);
        ActiveCountDown(6);
        //Debug.Log("He's learning a new job"+ resident.GetComponent<H_Resident>().tag);
        StartCoroutine(WaitForBecomeHarvester());
    }
    public void OnClickBecomeLumberjack()
    {
        panelJobSelection.SetActive(false);
        ActiveCountDown(6);
        //Debug.Log("He's learning a new job"+ resident.GetComponent<H_Resident>().tag);
        StartCoroutine(WaitForBecomeLumberjack());
    }
    public void OnClickBecomeMinor()
    {
        panelJobSelection.SetActive(false);
        ActiveCountDown(6);
        //Debug.Log("He's learning a new job" + resident.GetComponent<H_Resident>().tag);
        StartCoroutine(WaitForBecomeMinor());
    }

    #region coroutineJob
    IEnumerator WaitForBecomeBuilder()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Builder);
    }
    IEnumerator WaitForBecomeLumberjack()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Lumberjack);
    }
    
    IEnumerator WaitForBecomeHarvester()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Harvester);
    }
    IEnumerator WaitForBecomeMinor()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Minor);
    }
    #endregion
    
    
    public void Pause()
    {
        Debug.Log(Time.timeScale);
        if(!pause)
        {
            Time.timeScale = 0;
            fastForward = false;
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        pause = false;
        fastForward = false;
    }
    public void FastForwardQuickly(float num)
    {
        pause = false;
        Time.timeScale = num;
        timeSpent += Time.timeScale;
    }
    public void FastForwardSoMuch(float num)
    {
        pause = false;
        Time.timeScale = num;
        timeSpent += Time.timeScale;
    }
}
