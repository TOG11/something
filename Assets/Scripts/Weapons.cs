using GameSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject BulletPrefab;
    public GameObject EnemyBulletPrefab;

    [Header("Settings")]
    public float bullet_speed = 300.0f;
    public int EnemyBulletIntervalInSecinds = 1;

    private List<Action> PlayerCallbacks = new();
    private List<GameClasses.Enemy> enemies = new();
    internal List<GameObject> enemy_temp_parent = new();
    internal GameObject player_temp_parent;
    internal bool awaiting;

    public static Weapons singleton;

    private void Awake()
    {
        singleton = this;
    }

    internal GameObject temp_parent;

    private void Start()
    {
        PlayerCallbacks.Add(() => { RunPlayerCallbacks(Spawner.singleton.player); });
    }

    private void RunPlayerCallbacks(GameClasses.Player player)
    {
        if (Input.GetMouseButtonDown(0) && !player.shoot)
        {
            player_temp_parent = new GameObject("BulletParent");

            foreach (GameObject gb in player.GunBarrels)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = gb.transform.position;
                bullet.transform.parent = player_temp_parent.transform;
            }

            Vector3 look_dir = player.instance.transform.forward;
            player_temp_parent.transform.parent = player.instance.transform;
            player_temp_parent.transform.LookAt(look_dir);
            player_temp_parent.transform.Translate(Vector3.forward * bullet_speed * Time.deltaTime);
            player_temp_parent.transform.parent = null;

            Rigidbody rb = player_temp_parent.AddComponent<Rigidbody>();
            rb.drag = 0;
            rb.useGravity = false;
            rb.velocity = look_dir * 80;

            ShootCooldowm(player);
        }
    }

    private void RunEnemyCallbacks()
    {
        foreach (GameClasses.Enemy enemy in enemies)
        {
            if (!enemy.shoot)
            {
                List<GameObject> bullets = new List<GameObject>();
                foreach (GameObject gb in enemy.GunBarrels)
                {
                    GameObject bullet = Instantiate(EnemyBulletPrefab);
                    bullet.transform.position = gb.transform.position;
                    bullets.Add(bullet);
                }

                GameObject temp_parent = new GameObject("BulletParent");
                enemy_temp_parent.Add(temp_parent);

                foreach (GameObject bullet in bullets)
                {
                    Rigidbody rb = bullet.AddComponent<Rigidbody>();
                    rb.drag = 0;
                    rb.useGravity = false;
                    rb.velocity = new Vector3(0, 0, -1) * UnityEngine.Random.Range(50, 80);

                    ShootCooldowm(null, enemy, temp_parent);
                }
            }
        }
    }

    private void ShootCooldowm(GameClasses.Player player = null, GameClasses.Enemy enemy = null, GameObject temp_parent = null)
    {
        if (player != null)
        {
            StartCoroutine(FuncUtils.Wait(2f, () => { player.shoot = false; player_temp_parent = null; }));
        }
        else
        {
            enemy.shoot = false;
            enemy_temp_parent.Remove(temp_parent);
            Destroy(temp_parent);
        }
    }

    private void Update()
    {
        enemies = Spawner.singleton.enemies; /* Removing this causes enemies not to shoot */
        PlayerCallbacks.ForEach((A) => { A.Invoke(); });
        if (!awaiting)
        {
            awaiting = true;
            StartCoroutine(FuncUtils.Wait(EnemyBulletIntervalInSecinds, () => { RunEnemyCallbacks(); awaiting = false; }));
        }
    }
}
