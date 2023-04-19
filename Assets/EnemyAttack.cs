using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange;
    public float attackSpeed;
    float attackCooldown;
    public Transform attackPoint;

    public float damage = 10;

    public LayerMask playerLayer;

    public Animator animator;

    void Update()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if(transform.GetComponent<SkeletonMovement>().isFollowingPlayer && Vector2.Distance(attackPoint.position, transform.GetComponent<SkeletonMovement>().target.position) <= attackRange && attackCooldown <= 0)
        {
            attackCooldown = attackSpeed;
            Attack();
        }
    }

    public void Attack()
    {
        if (transform.GetComponent<SkeletonMovement>().angleOriginal > 0 && transform.GetComponent<SkeletonMovement>().angleOriginal < 45)
        {
            animator.SetTrigger("AttackRight");
        }
        else if (transform.GetComponent<SkeletonMovement>().angleOriginal > 45 && transform.GetComponent<SkeletonMovement>().angleOriginal < 135)
        {
            animator.SetTrigger("AttackUp");
        }
        else if (transform.GetComponent<SkeletonMovement>().angleOriginal > 135 && transform.GetComponent<SkeletonMovement>().angleOriginal < 180)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (transform.GetComponent<SkeletonMovement>().angleOriginal > -180 && transform.GetComponent<SkeletonMovement>().angleOriginal < -135)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (transform.GetComponent<SkeletonMovement>().angleOriginal > -135 && transform.GetComponent<SkeletonMovement>().angleOriginal < -45)
        {
            animator.SetTrigger("AttackDown");
        }
        else if (transform.GetComponent<SkeletonMovement>().angleOriginal > -45 && transform.GetComponent<SkeletonMovement>().angleOriginal < 0)
        {
            animator.SetTrigger("AttackRight");
        }

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerController>().TakeDamage(damage, gameObject);
        }
    }
}
//transform.GetComponent<SkeletonMovement>().target