using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            if(transform.GetComponent<SkeletonDie>() != null)
            {
                transform.GetComponent<SkeletonDie>().Die();
            }
            else
            {
                animator.SetTrigger("Die");
                Destroy(gameObject);
            }

        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //animator.SetTrigger("TakeDamage");
    }

    public void Heal(float health)
    {
        currentHealth += health;
    }
}
