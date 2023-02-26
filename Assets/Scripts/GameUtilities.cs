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
            public GameObject instance;
            public GameObject prefab;
            public GameUtilities.Health Health;
            public List<GameObject> GunBarrels = new List<GameObject>();
        }

        [Serializable]
        public class Player
        {
            internal bool shoot;
            public GameObject instance;
            public GameObject prefab;
            public GameUtilities.Health Health;
            public List<GameObject> GunBarrels = new List<GameObject>();
        }
    }

    [Serializable]
    public class FuncUtils
    {
        Transform GetClosestEnemy(Vector3 fromPos)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = fromPos;
            foreach (GameClasses.Enemy enemy in Spawner.singleton.enemies)
            {
                Transform t = enemy.instance.transform;
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
    }
};