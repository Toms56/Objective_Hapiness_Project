using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float scrollSpeed;
    float screenBorderDeviation = 10f;

    public Vector3 pannelLimite;
    //float fieldOfView = 141f;

    // For scrolling Orthographic
    private Camera cam;
    private float targetZoom;
    public float ZoomLerpSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        // Move the camera when the mouse is in a corner
        if (Input.mousePosition.y >= Screen.height - screenBorderDeviation)
        {
            position.y += 1 * speed * Time.deltaTime;
            //transform.Translate(0, 1 * speed * Time.deltaTime, 0, Space.World);
        }

        if (Input.mousePosition.y <= screenBorderDeviation)
        {
            position.y -= 1 * speed * Time.deltaTime;
            //transform.Translate(0, -1 * speed * Time.deltaTime, 0, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width - screenBorderDeviation)
        {
            position.x += 1 * speed * Time.deltaTime;
            //transform.Translate(1 * speed * Time.deltaTime, 0, 0, Space.World);
        }

        if (Input.mousePosition.x <= screenBorderDeviation)
        {
            position.x -= 1 * speed * Time.deltaTime;
            //transform.Translate(-1 * speed * Time.deltaTime, 0, 0, Space.World);
        }

        
        // Scrolling for Camera Perspective
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //position.z += scroll * scrollSpeed * Time.deltaTime;
        // Second method
         //Camera.main.fieldOfView += scroll * scrollSpeed * Time.deltaTime;

        // Scrolling for Camera Orthographic
        targetZoom -= scroll * scrollSpeed;
        targetZoom = Mathf.Clamp(targetZoom, 2f, 5f); // Clamp for Camera Orthographic
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * ZoomLerpSpeed);

        // Clamp between two value the position on the different axes
        position.x = Mathf.Clamp(position.x, -pannelLimite.x, pannelLimite.x);
        position.y = Mathf.Clamp(position.y, -pannelLimite.y, pannelLimite.y);

        // Clamp for scrolling Camera Perspective
        //position.z = Mathf.Clamp(position.z, -pannelLimite.z, pannelLimite.z);
            //Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 138f, 144f);

        transform.position = position;
    }
}
