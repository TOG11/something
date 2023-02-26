using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class Bullet : MonoBehaviour
{
    internal bool stop;
    void FixedUpdate()
    {
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && !stop)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Spawner.singleton.Enemys.ForEach((enemy) =>
            {
                if (hit.transform.gameObject.tag == "SHIP")
                {
                    enemy.Health.RemoveHealth(Random.Range(0, 20));
                    if (enemy.Health.HP <= 0)
                    {
                        Destroy(hit.transform.gameObject);
                    }   
                    else
                        stop = true;
                }
            });
        }
    }

    private void Update()
    {
        if (transform.position.z > 200f)
            Destroy(transform.parent.gameObject);
    }
}
