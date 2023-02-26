using UnityEngine;
using GameSystem;
using System.Collections.Generic;
using TMPro;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject player_prefab = null;
    public GameObject enemy_prefab = null;

    [Header("Starting positions")]
    public Vector3 player_start_pos = Vector3.zero;
    public Vector2 enemy_vert_pos_range = new Vector2(-10, 10);
    public Vector2 enemy_horiz_pos_range = new Vector2(-10, 10);
    public Vector2 enemy_z_range = new Vector2(50, 70);

    [Header("Misc")]
    public GameObject Target;
    public TextMeshProUGUI score_text;

    public GameClasses.Player player = new GameClasses.Player();
    public List<GameClasses.Enemy> enemies = new List<GameClasses.Enemy>();

    public static Spawner singleton = null;
    /* Wave starts at zero with no enemies on screen. */
    private int current_wave = 0;

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
        enemy_prefab.name += "_"+Random.value;
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
        FuncUtils.RemoveEnemyCallback(enemies);
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
        FuncUtils.AddEnemyCallback(enemies);
    }

    private void Update()
    {
        Target.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 32));

        if (enemies.Count == 0)
        {
            current_wave += 1;
            if (current_wave == 69 || current_wave == 420 || current_wave == 69420)
                score_text.text = $"Score: {current_wave} (Nice!)";
            else
                score_text.text = $"Score: {current_wave}";

            spawn_enemy_wave();
        }
    }
}
