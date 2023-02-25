using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using System;

public class Spawner : MonoBehaviour
{
    public static Spawner singleton;
    internal GameUtilitys.Health Health = new GameUtilitys.Health();
    public List<GameClasses.Player> Players = new List<GameClasses.Player>();
    public Vector3 PlayerStartingPosition;
    public Vector3 EnemyStartingPosition;
    public List<GameClasses.Enemey> Enemys = new List<GameClasses.Enemey>();

    private void Awake()
    {
        //initialize players
        foreach (var i in Players)
        {
            var p = Instantiate(i.playerPrefab);
            i.playerInstance = p;
            p.transform.position = PlayerStartingPosition;
            i.Health = new GameUtilitys.Health();
            i.GunBarrels.ForEach((barrel) =>
            {
                var b = Instantiate(barrel);
                b.transform.parent = p.transform;
            });
        }

        foreach (var i in Enemys)
        {
            var p = Instantiate(i.enemyPrefab);
            i.enemyInstance = p;
            p.transform.position = EnemyStartingPosition;
            i.Health = new GameUtilitys.Health();
            i.GunBarrels.ForEach((barrel) =>
            {
                var b = Instantiate(barrel);
                b.transform.parent = p.transform;
            });
        }
        singleton = this;
    }
}
