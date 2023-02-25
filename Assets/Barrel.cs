using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    void FixedUpdate()
    {
        int layerMask = 1 << 8;

        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            print("target aquired");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.gameObject.tag == "SHIP")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Vector3 screenPoint = hit.transform.position;
                screenPoint.z = 10.0f;
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(screenPoint);
                GameObject.FindGameObjectWithTag("TARGET").transform.position = screenPos;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

        }
    }
}
