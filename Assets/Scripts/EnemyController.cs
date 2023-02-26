using UnityEngine;
using GameSystem;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;

    private GameClasses.Player player;
    private bool move_to_player = true;

    private void Awake()
    {
        player = Spawner.singleton.player;
    }

    private void Update()
    {
        Vector3 player_pos = player.instance.transform.position;
        float distance_from_player = Vector3.Distance(transform.position, player_pos);

        float step = speed * Time.deltaTime;

        if (distance_from_player >= 5.0f && move_to_player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player_pos, step);
        }
        else
        {
            move_to_player = false;
        }

        if (!move_to_player)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, pos.z - step);

            /* Remove enemy when it passes past the camera, also possibly
             * doing damage to the player? */
            if (transform.position.z < Camera.main.transform.position.z)
                Spawner.singleton.remove_enemy(gameObject);
        }
    }
}
