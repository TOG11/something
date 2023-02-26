using GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float speed = 300.0f;

    public static Weapons singleton;
    public List<Action> Callbacks = new List<Action>();

    internal GameObject temp_parent;

    private void Start()
    {
        Callbacks.Add(() => { RunPlayerCallbacks(Spawner.singleton.player); });
    }

    public void RunPlayerCallbacks(GameClasses.Player player)
    {
        if (Input.GetMouseButtonDown(0) && !player.shoot)
        {
            temp_parent = new GameObject("BulletParent");
            foreach (GameObject gb in player.GunBarrels)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = gb.transform.position;
                bullet.transform.parent = temp_parent.transform;
            }

            Vector3 forward_dir = player.instance.transform.forward;

            temp_parent.transform.parent = player.instance.transform;
            temp_parent.transform.LookAt(forward_dir);
            temp_parent.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            temp_parent.transform.parent = null;

            Rigidbody rb = temp_parent.AddComponent<Rigidbody>();
            rb.drag = 0;
            rb.useGravity = false;
            rb.velocity = forward_dir * 80;

            Shoot(player);
        }
    }

    public void Shoot(GameClasses.Player player)
    {
        StartCoroutine(Wait(2.0f, () => { player.shoot = false; temp_parent = null; }));
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
