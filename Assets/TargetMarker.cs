using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarker : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (target)
        {
            Vector3 position;
            position.x = target.transform.position.x;
            position.y = target.transform.position.y + 1;
            position.z = target.transform.position.z;
            transform.position = position;
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void SetTarget(GameObject targetToSet)
    {
        target = targetToSet;
    }
}
