using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float range;
    public LayerMask playerLayer;

    public float damage = 0.01f;

    void Update()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, range, playerLayer);

        if(hitPlayers != null)
        {
            foreach(Collider2D player in hitPlayers){
                player.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }
}
