using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GameSystem
{
    [Serializable]
    public class GameUtilities : MonoBehaviour
    {
        [Serializable]
        public class Health
        {
            public float HP = 100;
            public float AddHealth(float add)
            {
                HP += add;
                return HP;
            }

            public float RemoveHealth(float rem)
            {
                HP -= rem;
                return HP;
            }
        }
        [Serializable]
        public class Ammo
        {
            public static int Mag = 25;
            public int AddAmmo(int add)
            {
                Mag += add;
                return Mag;
            }

            public int RemoveHealth(int rem)
            {
                Mag -= rem;
                return Mag;
            }
        }
    }
    [Serializable]
    public class GameClasses
    {
        [Serializable]
        public class Enemy
        {
            internal bool shoot;
            public GameObject enemyInstance;
            public GameObject enemyPrefab;
            public GameUtilities.Health Health;
            public List<GameObject> GunBarrels;
        }
        [Serializable]
        public class Player
        {
            internal bool shoot;
            public GameObject playerInstance;
            public GameObject playerPrefab;
            public GameUtilities.Health Health;
            public List<GameObject> GunBarrels;
        }
    }
    [Serializable]
    public class FuncUtils
    {
        static Transform GetClosestEnemy(Vector3 fromPos)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = fromPos;
            foreach (GameClasses.Enemy enemy in Spawner.singleton.Enemys)
            {
                Transform t = enemy.enemyInstance.transform;
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }

        static public void HideTarget(bool hide)
        {
            Spawner.singleton.Target.SetActive(!hide);
        }
    }
};