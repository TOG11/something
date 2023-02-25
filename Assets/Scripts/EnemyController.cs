using UnityEngine;
using GameSystem;
public class EnemyController : MonoBehaviour
{
    internal Camera cam;
    public GameClasses.Player TargetPlayer;
    public bool moveTowards = true;
    public float speed = 5.0f;

    private void Awake()
    {
        if (Spawner.singleton.Players.Count - 1 >= 1)
            TargetPlayer = Spawner.singleton.Players[Random.Range(0, Spawner.singleton.Players.Count - 1)];
        else
            TargetPlayer = Spawner.singleton.Players[0];
        cam = Camera.main;
    }


    internal bool set_speed;
    internal bool go_to_cam;
    private void Update()
    {
        if (moveTowards)
        {
            var step = speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, TargetPlayer.playerInstance.transform.position) > 6 && !go_to_cam)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetPlayer.playerInstance.transform.position, step);
            }
            else if (Vector3.Distance(transform.position, Camera.main.transform.position) > 0.001f)
            {
                go_to_cam = true;
                if (!set_speed)
                {
                    set_speed = true;
                    speed = (speed * 2) + 3;
                }
                transform.Rotate(Camera.main.transform.rotation.eulerAngles * Time.deltaTime * speed);
                transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, step);
            }
            if (Vector3.Distance(transform.position, Camera.main.transform.position) < 0.001f)
            {
                moveTowards = false;
            }
        }
    }
}
