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
        public static IEnumerator<WaitForSeconds> Wait(float time, Action func)
        {
            yield return new WaitForSeconds(time);
            func.Invoke();
        }

        public static IEnumerator<WaitForEndOfFrame> Looper(int loopTimes, Action funcToLoop)
        {
            for (int i = 0; i < loopTimes; i++)
            {
                funcToLoop.Invoke();
            }
            yield return new WaitForEndOfFrame();
        }

        public static Transform GetClosestEnemy(Vector3 fromPos)
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

        public static void HideTarget(object hide=null)
        {
            Spawner.singleton.TargetParent.SetActive(!(bool)hide);
        }
    }
};