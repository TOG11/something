using GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject EnemyBulletPrefab;
    public float speed = 300.0f;
    internal GameObject PLAYER_TEMP_PARENT;
    internal List<GameObject> ENEMY_TEMP_PARENT = new List<GameObject>();
    public static Weapons singleton;
    public List<Action> PlayerCallbacks = new List<Action>();
    public List<Action> EnemyCallbacks = new List<Action>();
    public int EnemyBulletIntervalInSecinds = 1;

    private void Awake()
    {
        singleton = this;
    }

    internal GameObject temp_parent;

    private void Start()
    {
        PlayerCallbacks.Add(() => { RunPlayerCallbacks(Spawner.singleton.player); });
    }

    internal void AddEnemyCallback(List<GameClasses.Enemy> e)
    {
        EnemyCallbacks.Add(() => { RunEnemyCallbacks(e); });
    }

    internal void RemoveEnemyCallback(List<GameClasses.Enemy> e)
    {
        EnemyCallbacks.Remove(() => { RunEnemyCallbacks(e); });
    }

    public void RunPlayerCallbacks(GameClasses.Player player)
    {
        if (Input.GetMouseButtonDown(0) && !player.shoot)
        {
            PLAYER_TEMP_PARENT = new GameObject("BulletParent");
            foreach (var gb in player.GunBarrels)
            {
                GameObject bullet = Instantiate(EnemyBulletPrefab);
                bullet.transform.position = gb.transform.position;
                bullet.transform.parent = PLAYER_TEMP_PARENT.transform;
            }
            var LookingTowards = player.instance.transform.forward;
            PLAYER_TEMP_PARENT.transform.parent = player.instance.transform;
            PLAYER_TEMP_PARENT.transform.LookAt(LookingTowards);
            PLAYER_TEMP_PARENT.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            var r = PLAYER_TEMP_PARENT.AddComponent<Rigidbody>();
            r.drag = 0;
            r.useGravity = false;
            PLAYER_TEMP_PARENT.transform.parent = null;
            r.velocity = LookingTowards * 80;
            Shoot(player);
        }
    }

    public void RunEnemyCallbacks(List<GameClasses.Enemy> enemys)
    {
        foreach (var enemy in enemys)
        {
            if (!enemy.shoot)
            {
                GameObject TEMP_PARENT = new GameObject("BulletParent");
                ENEMY_TEMP_PARENT.Add(TEMP_PARENT);
                List<GameObject> bullets = new List<GameObject>();
                foreach (var gb in enemy.GunBarrels)
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    bullet.transform.position = gb.transform.position;
                    bullets.Add(bullet);
                }

                var LookingTowards = Spawner.singleton.player.instance.transform.position;
                foreach (GameObject bullet in bullets)
                {
                    var r = bullet.AddComponent<Rigidbody>();
                    r.drag = 0;
                    r.useGravity = false;
                    r.velocity = new Vector3(0, 0, -1) * UnityEngine.Random.Range(50, 80);
                    Shoot(null, enemy, TEMP_PARENT);
                }
            }
        }
    }

    public void Shoot(GameClasses.Player player = null, GameClasses.Enemy enemy = null, GameObject TEMP_PARENT = null)
    {
        if (player != null)
            StartCoroutine(FuncUtils.Wait(2f, () => { player.shoot = false; PLAYER_TEMP_PARENT = null; }));
        else
        {
            enemy.shoot = false; ENEMY_TEMP_PARENT.Remove(TEMP_PARENT);
        }
    }

    private void Update()
    {
        PlayerCallbacks.ForEach((A) => { A.Invoke(); });
        if (!awaiting)
        {
            awaiting = true;
            StartCoroutine(FuncUtils.Wait(EnemyBulletIntervalInSecinds, () => { EnemyCallbacks.ForEach((A) => { A.Invoke(); awaiting = false; }); }));
        }
    }
    internal bool awaiting;
}
