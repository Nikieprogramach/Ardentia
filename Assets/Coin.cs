using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            collider.GetComponent<PlayerController>().AddMoney(1);
            Destroy(gameObject);
        }
    }
}
