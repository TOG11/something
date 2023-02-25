using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System;
using GameSystem;

public class Weapons : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float speed;
    internal GameObject TEMP_PARENT;
    public static Weapons singleton;
    public List<Action> Callbacks = new List<Action>();

    private void Start()
    {
        foreach (GameClasses.Player player in Spawner.singleton.Players)
            Callbacks.Add(() => { RunPlayerCallbacks(player); });
    }

    public void RunPlayerCallbacks(GameClasses.Player player)
    {
        if (Input.GetMouseButtonDown(0) && !player.shoot)
        {
            TEMP_PARENT = new GameObject("BulletParent");
            foreach (var gb in player.GunBarrels)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = gb.transform.position;
                bullet.transform.parent = TEMP_PARENT.transform;
            }
            var LookingTowards = player.playerInstance.transform.forward;
            TEMP_PARENT.transform.parent = player.playerInstance.transform;
            TEMP_PARENT.transform.LookAt(LookingTowards);
            TEMP_PARENT.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            var r = TEMP_PARENT.AddComponent<Rigidbody>();
            r.drag = 0;
            r.useGravity = false;
            r.velocity = LookingTowards * 80;
            player.shoot = true;
        }
        if (player.shoot)
        {
            //delay to shoot again & delete bullet after shoot
            StartCoroutine(Wait(0.7f, () => { Destroy(TEMP_PARENT); player.shoot = false; TEMP_PARENT = null; }));
        }
    }

    private void Update()
    {
        Callbacks.ForEach((A) => { A.Invoke(); });
    }

    public IEnumerator Wait(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func.Invoke();
    }
}
