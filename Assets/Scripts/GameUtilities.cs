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
        static public void HideTarget(bool hide)
        {
            Spawner.singleton.Target.SetActive(!hide);
        }
    }
};