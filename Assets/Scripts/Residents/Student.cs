using UnityEngine;

public class Student : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 school;
    private GameObject homeStudent;
    private bool studying;


    private void Awake()
    {
        school = GameManager.Instance.school.transform.position;
        homeStudent = GameManager.Instance.home;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(school);
            
            if (Vector3.Distance(transform.position,school) <= 0.5f && !studying)
            {
                studying = true;
            }
        }
        else
        {
            studying = false;
            resident.agent.SetDestination(homeStudent.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
