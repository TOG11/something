using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float speed = 5.0f;

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
    }
}
