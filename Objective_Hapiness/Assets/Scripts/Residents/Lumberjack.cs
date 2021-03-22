using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 forest;
    private GameObject homelumberjack;

    private void Awake()
    {
        forest = GameManager.Instance.forestWaypoint.transform.position;
        homelumberjack = GameManager.Instance.home;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.day)
        {
            resident.agent.SetDestination(forest);
            
            if (Vector3.Distance(transform.position,forest) <= 1f)
            {
                //Debug.Log("Lumberjack in forest");
            }
        }
        else
        {
            resident.agent.SetDestination(homelumberjack.transform.position);
        }
    }
}
