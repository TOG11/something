using GameSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject player_prefab = null;
    public GameObject enemy_prefab = null;

    [Header("Starting positions")]
    public Vector3 player_start_pos = Vector3.zero;
    public Vector2 enemy_vert_pos_range = new(-10, 10);
    public Vector2 enemy_horiz_pos_range = new(-10, 10);
    public Vector2 enemy_z_range = new(50, 70);

    [Header("Objects")]
    public GameObject Target;
    public GameObject TargetParent;
    public GameObject EnemyHealth;
    public TextMeshProUGUI wave_text;
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI health_level;

    [System.NonSerialized]
    public GameClasses.Player player = new();
    [System.NonSerialized]
    public List<GameClasses.Enemy> enemies = new();

    /* Wave starts at zero with no enemies on screen. */
    private int current_wave = 0;
    private int score = 0;
    public static Spawner singleton = null;
    private void create_player()
    {
        GameObject plr = Instantiate(player_prefab);

        foreach (Transform barrel in plr.GetComponentsInChildren<Transform>())
            if (barrel.gameObject.CompareTag("BARREL"))
                player.GunBarrels.Add(barrel.gameObject);

        plr.transform.position = player_start_pos;
        player.instance = plr;
        player.Health = new GameUtilities.Health();
    }

    private void create_enemy(Vector3 pos)
    {
        GameClasses.Enemy enemy = new GameClasses.Enemy();
        enemy_prefab.name += "_" + Random.value;
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

    public void kill_enemy(GameObject go)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].instance == go)
            {
                score += 1;
                score_text.text = $"Score: {score}";

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
            float x = Random.Range(enemy_horiz_pos_range.x, enemy_horiz_pos_range.y);
            float y = Random.Range(enemy_vert_pos_range.x, enemy_vert_pos_range.y);
            float z = Random.Range(enemy_z_range.x, enemy_z_range.y);
            create_enemy(new Vector3(x, y, z));
        }
    }

    internal Vector3 enemyHEalthGUIstartPos;
    private void Update()
    {
        Target.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 32));
        EnemyHealth.transform.position = enemyHEalthGUIstartPos;

        health_level.text = $"Health: {player.Health.HP}";

        if (enemies.Count == 0)
        {
            current_wave += 1;

            wave_text.text = $"Wave: {current_wave}";
            if (current_wave == 69 || current_wave == 420 || current_wave == 69420)
                wave_text.text += " (Nice!)";

            spawn_enemy_wave();
        }
    }
}
