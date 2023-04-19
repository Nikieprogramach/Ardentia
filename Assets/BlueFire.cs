using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : MonoBehaviour
{
    public float duration;
    public int damage;

    void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            collider.GetComponent<PlayerController>().TakeDamage(damage, null);
        }
        else if(collider.GetComponent<Enemy>() != null && collider.GetComponent<SkeletonBossAttack>())
        {
            collider.GetComponent<Enemy>().Heal(100);
            Destroy(gameObject);
        }
    }
}
