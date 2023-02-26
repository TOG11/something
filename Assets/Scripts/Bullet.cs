using UnityEngine;
using GameSystem;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [Range(0, 50)]
    public int health_min = 3;
    [Range(0, 50)]
    public int health_max = 7;

    public bool IsEnemy;
    internal List<GameClasses.Enemy> enemies;
    internal bool stop;

    private void Awake()
    {
        enemies = Spawner.singleton.enemies;
    }

    private void FixedUpdate()
    {
        if (!IsEnemy)
        {
            int layerMask = ~(1 << 8);

            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity, layerMask) && !stop)
            {
                Debug.DrawRay(transform.position, forward * hit.distance, Color.yellow);
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (hit.transform.gameObject.CompareTag("SHIP"))
                    {
                        enemies[i].Health.RemoveHealth(Random.Range(health_min, health_max));
                        if (enemies[i].Health.HP <= 0.0f)
                            Spawner.singleton.remove_enemy(hit.transform.gameObject);
                        else
                            stop = true;
                    }
                }
            }
        }
        else if (IsEnemy)
        {
            /*
            int layerMask = ~(1 << 8);

            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity, layerMask) && !stop)
            {
                Debug.DrawRay(transform.position, forward * hit.distance, Color.yellow);
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        player.Health.RemoveHealth(Random.Range(health_min, health_max));
                        if (enemies[i].Health.HP <= 0.0f)
                            Spawner.singleton.remove_enemy(hit.transform.gameObject);
                        else
                            stop = true;
                    }
            }
            */
        }
    }

    private void delete_bullet()
    {
        Destroy(transform.parent.gameObject);
    }

    private void Update()
    {
        if (transform.position.z > 200.0f)
            delete_bullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        delete_bullet();
    }
}
