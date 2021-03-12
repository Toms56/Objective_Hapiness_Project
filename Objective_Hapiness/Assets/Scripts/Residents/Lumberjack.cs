using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    private H_Resident resident;
    private Vector3 forest;

    private void Awake()
    {
        resident = GetComponent<H_Resident>();
        forest = GameManager.Instance.forestWaypoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(forest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
