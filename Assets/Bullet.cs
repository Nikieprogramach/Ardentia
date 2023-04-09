using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime;
    public float damage;

    void Update()
    {
        if(lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }else if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() == null)
        {
            //Enemy enemy = collider.GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //    enemy.TakeDamage(damage);
            //}
            Destroy(gameObject);
        }
    }
}
