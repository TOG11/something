using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    [Serializable]
    public class GameUtilities : MonoBehaviour
    {
        [Serializable]
        public class Health
        {
            public float HP = 100.0f;

            public float AddHealth(float f)
            {
                return HP += f;
            }

            public float RemoveHealth(float f)
            {
                return HP -= f;
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