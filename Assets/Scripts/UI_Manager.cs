using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables
    
    public static UI_Manager Instance;
    
    private GameObject resident;
    //CountDown TimeManagement
    private float startingTime;
    //[SerializeField]private float totalTime;
    
    [SerializeField] private Camera cam;
    //UI Management
    [SerializeField] GameObject panelJobSelection;
    [SerializeField] GameObject panelSelectedPnj;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelWinGame;
    [SerializeField] Button jobButton;
    [SerializeField] Button schoolButton;
    [SerializeField] Button constructionButton;
    [SerializeField] Text foodText;
    [SerializeField] Text woodText;
    [SerializeField] Text stoneText;
    [SerializeField] Text ageText;
    [SerializeField] Text jobText;
    [SerializeField] Text countDownTxt;
    [SerializeField] Text dayCycleText;
    [SerializeField] Text nightCycleText;
    [SerializeField] Text countDownDayCycle;
    [SerializeField] Text textnews;
    //ProsperityBar Management
    [SerializeField] Slider prosperityBar;

    private float minutes;
    private float seconds;

    //public bool useEventTimer;

    private GameObject selectedResident;

    private bool play;
    
    //DayTimeManagement
    public float durationDay;
    public float durationNight;
    [SerializeField] float timeSpent;
    
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
        Time.timeScale = 1;
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
        if (timeSpent > durationDay && GameManager.day)
        {
            GameManager.day = false;
            timeSpent = 0;
        }
        else if (timeSpent > durationNight && !GameManager.day)
        {
            GameManager.day = true;
            timeSpent = 0;
            constructionButton.interactable = true;
        }
        if (GameManager.day)
        {
            countDownDayCycle.text = "DayTime : " + Mathf.RoundToInt(durationDay - timeSpent);   
        }
        else if (!GameManager.day)
        {
            countDownDayCycle.text = "NightTime : " + Mathf.RoundToInt(durationNight - timeSpent);
        }
        if (GameManager.day && Mathf.RoundToInt(durationDay - timeSpent) -12 <= 0)
        {
            constructionButton.interactable = false;
        }
        #endregion
        //actualization of prosperity
        prosperityBar.value = GameManager.prosperity;

        #region RessourcesManagement
        foodText.text = " " + GameManager.food;
        woodText.text = " " + GameManager.wood;
        stoneText.text = " " + GameManager.stone;
        #endregion

        if (Input.GetButtonDown("Fire1"))
        {
            SelectResident();
        }
        
        //Player doesn't change resident's job if a school is not builded before
        if (GameManager.schoolBuilded && panelSelectedPnj.activeSelf)
        {
            jobButton.interactable = true;
        }
        else
        {
            jobButton.interactable = false;
        }

        if (GameManager.schoolBuilded)
        {
            schoolButton.interactable = false;
        }

        #region winAndGameOver
        if (GameManager.gameOver)
        {
            panelGameOver.SetActive(true);
        }
        if (GameManager.victory)
        {
            panelWinGame.SetActive(true);
        }
        #endregion
    }

    public async void DayNightCycle()
    {
        int dayNum = 0;
        int nightNum = 0;
        while (!play)
        {
            dayCycleText.text = $"Days : {dayNum} ";
            nightCycleText.text = $"Nights : {nightNum}";
            if (GameManager.day)
            {
                await new WaitForSeconds(durationDay);
                //GameManager.day = false;
                dayNum += 1;
            }
            else
            {
                await new WaitForSeconds(durationNight);
                //GameManager.day = true;
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
                countDownTxt.text = "";
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
                break;
            case 1:
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                break;
            case 2 :
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                break;
            case 3 : 
                panelJobSelection.SetActive(false);
                GameManager.Instance.ToStudy(resident,GameManager.Works.Harvester);
                break;
        }  
    }

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

    public void OnClickResume()
    {
        Time.timeScale = 1;
    }
    
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }
    public void FastForwardQuickly()
    {
        Time.timeScale = 2;
        //timeSpent += Time.timeScale;
    }
    public void FastForwardSoMuch()
    {
        Time.timeScale = 3;
        //timeSpent += Time.timeScale;
    }

    public void EnoughFood()
    {
        textnews.text = "All residents were able to eat";
        StartCoroutine(ResetText());
    }
    
    public void ShowKillResidents(int kresidents)
    {
        textnews.text = $"You don't have enough food,\n{kresidents} residents are dead";
        StartCoroutine(ResetText());
    }

    public void NotEnoughRessources()
    {
        textnews.text = "You don't have the resources\nor enough builders";
        StartCoroutine(ResetText());
    }
    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(5);
        textnews.text = "News : ";
    }
}