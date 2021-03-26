using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables
    
    private GameObject resident;
    //CountDown TimeManagement
    private float startingTime;
    [SerializeField]private float totalTime;
    public Text countDownTxt;
    public Text dayCycleText;
    public Text nightCycleText;
    public Text countDownDayCycle;
    [SerializeField] Text textnews;

    private float minutes;
    private float seconds;

    public bool useEventTimer = false;
    
    //UI Management
    public GameObject panelJobSelection;
    public GameObject panelSelectedPnj;
    public GameObject panelGameOver;
    public GameObject panelWinGame;
    [SerializeField] Button jobButton;
    public Text foodText;
    public Text woodText;
    public Text stoneText;
    
    public Text ageText;
    public Text jobText;

    private GameObject selectedResident;
    public static UI_Manager Instance;
    

    [SerializeField] private Camera cam;
    private bool play;
    
    //DayTimeManagement
    public float durationDay;
    public float durationNight;
    [SerializeField]private float timeSpent;
    
    
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
        }
        else if (timeSpent > durationNight && !GameManager.Instance.day)
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

        #region RessourcesManagement
        foodText.text = " " + GameManager.food;
        woodText.text = " " + GameManager.wood;
        stoneText.text = " " + GameManager.stone;
        #endregion
        

        #region JobCountDownDisplay
        /*
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
        */
        #endregion

        if (Input.GetButtonDown("Fire1"))
        {
            SelectResident();
        }

        if (GameManager.Instance.schoolBuilded && panelSelectedPnj.activeSelf)
        {
            jobButton.interactable = true;
        }
        else
        {
            jobButton.interactable = false;
        }

        #region winAndGameOver

        if (GameManager.gameOver)
        {
            panelGameOver.SetActive(true);
        }
        if (GameManager.victory)
        {
            panelGameOver.SetActive(true);
        }

        #endregion
        
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
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                }
                else if (element.collider != null && element.collider.CompareTag("Builder"))
                {
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                }
                else if (element.collider != null && element.collider.CompareTag("Lumberjack"))
                {
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                }
                else if (element.collider != null && element.collider.CompareTag("Harvester"))
                {
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                }
                else if (element.collider != null && element.collider.CompareTag("Minor"))
                {
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                }
                else if (element.collider != null && element.collider.CompareTag("Student"))
                {
                    panelSelectedPnj.SetActive(true);
                    resident = element.collider.gameObject;
                    ageText.text = "Age : " + resident.GetComponent<H_Resident>().age;
                    jobText.text = " Job : " + resident.tag;
                    countDownTxt.text = $"Studying days : {resident.GetComponent<Student>().studyDays}";
                }
            }
        }
    }

    public void ChangeJob(int n)
    {
        switch (n)
        {
            case 0:
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Builder);
                //ActiveCountDown(6);
                //StartCoroutine(WaitForBecomeBuilder());
                break;
            case 1:
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                //ActiveCountDown(6);
                //StartCoroutine(WaitForBecomeHarvester());
                break;
            case 2 :
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                //ActiveCountDown(6);
                //StartCoroutine(WaitForBecomeLumberjack());
                break;
            case 3 : 
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                //ActiveCountDown(6);
                //StartCoroutine(WaitForBecomeMinor());
                break;
        }  
    }


    #region coroutineJob
    IEnumerator WaitForBecomeBuilder()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Builder);
        Debug.Log("He's has a new job"+ resident.GetComponent<H_Resident>().tag);
    }
    IEnumerator WaitForBecomeLumberjack()
    {
        yield return new WaitForSeconds(6);
        useEventTimer = false;
        GameManager.Instance.ChangeWork(resident, GameManager.Works.Lumberjack);
        Debug.Log("He's has a new job"+ resident.GetComponent<H_Resident>().tag);

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

    public void OnClickRetry()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void Pause()
    {
        Debug.Log(Time.timeScale);
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }
    public void FastForwardQuickly(float num)
    {
        Time.timeScale = num;
        timeSpent += Time.timeScale;
    }
    public void FastForwardSoMuch(float num)
    {
        Time.timeScale = num;
        timeSpent += Time.timeScale;
    }

    public void EnoughFood()
    {
        textnews.text = "All residents were able to eat";
        StartCoroutine(ResetText());
    }

    public void ShowKillResidents(int kresidents)
    {
        textnews.text = $"You don't have enough food, {kresidents} residents are dead";
        StartCoroutine(ResetText());
    }

    public void NotEnoughRessources()
    {
        textnews.text = "You don't have the resources";
    }
    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(3);
        textnews.text = "News : ";
    }
}