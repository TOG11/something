using GameSystem;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    private Vector3 get_mouse_to_world_pos()
    {
        Vector3 lookat_pos = Input.mousePosition;
        lookat_pos.x = Mathf.Clamp(lookat_pos.x, 0.0f, Screen.width);
        lookat_pos.y = Mathf.Clamp(lookat_pos.y, 0.0f, Screen.height);

        lookat_pos.z = transform.position.z - Camera.main.transform.position.z;
        lookat_pos = Camera.main.ScreenToWorldPoint(lookat_pos);

        return lookat_pos;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(
            transform.position,
            get_mouse_to_world_pos(), step);
    }

    
    private void FixedUpdate()
    {
        int layerMask = ~(1 << 8);

        RaycastHit hit;
        Vector3 castPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
        if (Physics.Raycast(castPoint, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            if (hit.transform.gameObject.CompareTag("SHIP"))
            {
                foreach (GameClasses.Enemy enemy in Spawner.singleton.enemies)
                {
                    if (enemy.instance == hit.transform.gameObject)
                    {
                        float HP = enemy.Health.HP;
                        Spawner.singleton.EnemyHealth.GetComponent<TextMeshProUGUI>().text = "Health: " + HP;
                    }
                }
                FuncUtils.HideTarget(false);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
                RectTransform enemyRect = hit.transform.gameObject.GetComponent<RectTransform>();
                RectTransform targetRect = GameObject.FindGameObjectWithTag("TARGET").GetComponent<RectTransform>();
                targetRect.position = new Vector3(enemyRect.position.x, enemyRect.position.y, enemyRect.position.z);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            FuncUtils.HideTarget(true);
        }
    }
}
