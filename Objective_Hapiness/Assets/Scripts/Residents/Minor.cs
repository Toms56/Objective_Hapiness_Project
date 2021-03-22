using UnityEngine;

public class Minor : MonoBehaviour
{
    [SerializeField] H_Resident resident;
    private Vector3 mine;

    private void Awake()
    {
        mine = GameManager.Instance.mineWaypoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        resident.agent.SetDestination(mine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
