using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDie : MonoBehaviour
{
    public GameObject blueFirePrefab;

    public void Die()
    {
        Instantiate(blueFirePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
