using UnityEngine;
using GameSystem;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject player_prefab = null;
    public GameObject enemy_prefab = null;

    [Header("Starting positions")]
    public Vector3 player_start_pos = Vector3.zero;
    public float enemy_z_pos = 50.0f;

    [System.NonSerialized]
    public GameClasses.Player player = new GameClasses.Player();
    [System.NonSerialized]
    public List<GameClasses.Enemy> enemies = new List<GameClasses.Enemy>();

    public static Spawner singleton = null;
    private int current_wave = 1;

    private void create_player()
    {
        GameObject plr = Instantiate(player_prefab);

        foreach (Transform barrel in plr.GetComponentsInChildren<Transform>())
            if (barrel.gameObject.tag == "BARREL")
                player.GunBarrels.Add(barrel.gameObject);

        plr.transform.position = player_start_pos;
        player.instance = plr;
        player.Health = new GameUtilities.Health();
    }

    private void create_enemy(Vector3 pos)
    {
        GameClasses.Enemy enemy = new GameClasses.Enemy();

        GameObject go = Instantiate(enemy_prefab);
        foreach (Transform barrel in go.GetComponentsInChildren<Transform>())
            if (barrel.gameObject.tag == "BARREL")
                enemy.GunBarrels.Add(barrel.gameObject);

        go.transform.position = pos;
        enemy.instance = go;
        enemy.Health = new GameUtilities.Health();

        enemies.Add(enemy);
    }

    public void remove_enemy(GameObject go)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].instance == go)
            {
                Destroy(enemies[i].instance);
                enemies.RemoveAt(i);
                break;
            }
        }
    }

    private void Awake()
    {
        singleton = this;
        create_player();
    }

    private void spawn_enemy_wave()
    {
        int enemy_count = current_wave * 2;
        for (int i = 0; i < enemy_count; i++)
        {
            float x = Random.Range(-10, 10);
            float y = Random.Range(-10, 10);
            create_enemy(new Vector3(x, y, enemy_z_pos));
        }
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            current_wave += 1;
            spawn_enemy_wave();
        }
    }

    private void Update()
    {
        Target.transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * 32));
    }
}
