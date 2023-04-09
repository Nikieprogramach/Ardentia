using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDungeon : MonoBehaviour
{
    public Transform dungeonTpPoint;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            collider.GetComponent<PlayerController>().transform.position = dungeonTpPoint.position;
        }
    }
}
