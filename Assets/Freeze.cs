using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public float duration;
    public bool isStun;
    public Transform player;
    float originalMoveSpeed;
    float originalDamage;

    void Start()
    {
        if (isStun)
        {
            originalMoveSpeed = player.GetComponent<PlayerController>().moveSpeed;
            originalDamage = player.GetComponent<Attacking>().damage;
            player.GetComponent<Attacking>().damage -= 5;
            player.GetComponent<PlayerController>().moveSpeed = 0;
        }
        else
        {
            originalMoveSpeed = player.GetComponent<PlayerController>().moveSpeed;
            player.GetComponent<PlayerController>().moveSpeed -= 3;
        }
    }

    void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            player.GetComponent<PlayerController>().moveSpeed = originalMoveSpeed;
            player.GetComponent<Attacking>().damage = originalDamage;
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<PlayerController>().feetPos.position, 3);
    }
}
