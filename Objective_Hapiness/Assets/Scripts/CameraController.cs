using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    float screenBorderDeviation = 10f;

    bool move = true;

    public Vector2 pannelLimite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > pannelLimite.x || transform.position.x < -pannelLimite.x)
        {
            move = !move;
        }
        if (!move)
        {
            return;
        }

        if (Input.mousePosition.y >= Screen.height - screenBorderDeviation)
        {
            transform.Translate(0, 1 * speed * Time.deltaTime, 0, Space.World);
        }

        if (Input.mousePosition.y <= screenBorderDeviation)
        {
            transform.Translate(0, -1 * speed * Time.deltaTime, 0, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width - screenBorderDeviation)
        {
            transform.Translate(1 * speed * Time.deltaTime, 0, 0, Space.World);
        }

        if (Input.mousePosition.x <= screenBorderDeviation)
        {
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0, Space.World);
        }    
    }
}
