using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Animator animator;

    Vector3 defaultPos;

    void Start()
    {
        currentHealth = maxHealth;
        defaultPos = transform.position;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            if(transform.GetComponent<SkeletonDie>() != null)
            {
                transform.GetComponent<SkeletonDie>().Die();
                Die();
            }
            else
            {
                DropLoot();
                Debug.Log("Died"); 
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
    
    public void ResetAgro()
    {
        transform.GetComponent<SkeletonMovement>().isFollowingPlayer = false;
        transform.position = defaultPos;
    }

    public void DropLoot()
    {
        DropLootController.instance.DropItem(transform);
    }

    public void Die()
    {
        QuestTracker.instance.OnEnemyKilled(gameObject);
        DropLootController.instance.DropItem(transform);
        Destroy(gameObject);
    }
}
