using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GameSystem
{
    [Serializable]
    public class GameUtilitys : MonoBehaviour
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
        public class Enemey
        {
            internal bool shoot;
            public GameObject enemyInstance;
            public GameObject enemyPrefab;
            public GameUtilitys.Health Health;
            public List<GameObject> GunBarrels;
        }
        [Serializable]
        public class Player
        {
            internal bool shoot;
            public GameObject playerInstance;
            public GameObject playerPrefab;
            public GameUtilitys.Health Health;
            public List<GameObject> GunBarrels;
        }
    }
};