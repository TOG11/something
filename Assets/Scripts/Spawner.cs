using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject Target;
    public static Spawner singleton;
    internal GameUtilities.Health Health = new GameUtilities.Health();
    public List<GameClasses.Player> Players = new List<GameClasses.Player>();
    public Vector3 PlayerStartingPosition;
    public Vector3 EnemyStartingPosition;
    public List<GameClasses.Enemy> Enemys = new List<GameClasses.Enemy>();

    private void Awake()
    {
        singleton = this;

        //initialize players
        foreach (var i in Players)
        {
            var p = Instantiate(i.playerPrefab);
            foreach (var barrel in p.GetComponentsInChildren<Transform>())
            {
                if (barrel.gameObject.tag == "BARREL")
                    i.GunBarrels.Add(barrel.gameObject);
            }
            i.playerInstance = p;
            p.transform.position = PlayerStartingPosition;
            i.Health = new GameUtilities.Health();
        }

        foreach (var i in Enemys)
        {
            var e = Instantiate(i.enemyPrefab);
            if (e.GetComponentsInChildren<Transform>()[0] != null)
                foreach (var barrel in e.GetComponentsInChildren<Transform>())
                {
                    if (barrel.gameObject.tag == "BARREL")
                        i.GunBarrels.Add(barrel.gameObject);
                }
            i.enemyInstance = e;
            e.transform.position = EnemyStartingPosition;
            i.Health = new GameUtilities.Health();
        }
    }

    private void Update()
    {
        Target.transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * 32));
    }
}
