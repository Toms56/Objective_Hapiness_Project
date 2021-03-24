using UnityEngine;

public class Student : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 school;
    private GameObject homeStudent;
    private bool studying;
    public int studyDays;
    public GameManager.Works studywork;


    private void Awake()
    {
        school = GameManager.Instance.school.transform.position;
        if (resident == null)
        {
            resident = gameObject.GetComponent<H_Resident>();
        }
        resident.hobo = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(school);
            
            if (Vector3.Distance(transform.position,school) <= 1f && !studying)
            {
                studying = true;
            }
        }
        else
        {
            studying = false;
            resident.agent.SetDestination(homeStudent.transform.position);
        }

        switch (studywork)
        {
            case GameManager.Works.Builder :
                studyDays = 2;
                break;
            case GameManager.Works.Harvester : 
                studyDays = 3;
                break;
            case GameManager.Works.Lumberjack :
                studyDays = 4;
                break;
            case GameManager.Works.Minor :
                studyDays = 1;
                break;
            default:
                Debug.LogError("pas de métier à étudier");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (studyDays <= 0)
        {
            GameManager.Instance.ChangeWork(gameObject,studywork);
        }
    }
}
