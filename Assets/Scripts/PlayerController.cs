using UnityEngine;
using GameSystem;
public class PlayerController : MonoBehaviour
{
    internal Camera cam;
    public float speed = 5.0f;
    private void Awake()
    {
        cam = Camera.main;
    }

    private Vector3 get_mouse_to_world_pos()
    {
        Vector3 lookat_pos = Input.mousePosition;
        lookat_pos.x = Mathf.Clamp(lookat_pos.x, 0.0f, Screen.width);
        lookat_pos.y = Mathf.Clamp(lookat_pos.y, 0.0f, Screen.height);

        lookat_pos.z = transform.position.z - cam.transform.position.z;
        lookat_pos = cam.ScreenToWorldPoint(lookat_pos);

        return lookat_pos;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(
            transform.position,
            get_mouse_to_world_pos(), step);

        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            Vector3 screenPoint = hit.transform.position;
            screenPoint.z = 10.0f; 
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(screenPoint);

            Canvas localCanvas = GameObject.FindGameObjectWithTag("LOCALCANVAS").GetComponent<Canvas>();
            RectTransform canvasRect = localCanvas.GetComponent<RectTransform>();
            RectTransform targetRect = GameObject.FindGameObjectWithTag("TARGET").GetComponent<RectTransform>(); ;

            

            if (hit.transform.gameObject.tag == "SHIP")
            {

            }
        }
        else
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
    }
}
