using GameSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [Range(0, 50)]
    public int health_min = 3;
    [Range(0, 50)]
    public int health_max = 7;

    [Range(0, 50)]
    public int player_health_min = 3;
    [Range(0, 50)]
    public int player_health_max = 7;

    public string death_scene;

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
                            Spawner.singleton.kill_enemy(hit.transform.gameObject);
                        else
                            stop = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("SHIP") && !other.gameObject.CompareTag("BARREL"))
            delete_bullet();

        if (other.gameObject.CompareTag("Player") && IsEnemy)
        {
            Spawner.singleton.player.Health.RemoveHealth(Random.Range(player_health_min, player_health_max));
            if (Spawner.singleton.player.Health.HP <= 0.0f)
            {
                SceneManager.LoadScene(death_scene);
            }
        }
    }

    private void delete_bullet()
    {
        Destroy(transform.gameObject);
    }

    private void Update()
    {
        if (transform.position.z > 200.0f || transform.position.z < Camera.main.transform.position.z)
            delete_bullet();
    }
}
