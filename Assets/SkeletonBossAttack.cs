using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossAttack : MonoBehaviour
{
    public bool isEngaged = false;

    public float attackRange;
    public float attackSpeed;
    float attackCooldown;
    public Transform attackPoint;

    public LayerMask playerLayer;

    public Animator animator;

    public float spawnSkeletons;
    float spawnSkeletonsCooldown;
    public GameObject skeletonPrefab;

    public float distanceToStun;
    public GameObject stunPrefab;
    public float slowCooldown;
    float slowCooldownCounter;
    public float stunCooldown;
    float stunCooldownCounter;


    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if(slowCooldownCounter > 0)
        {
            slowCooldownCounter -= Time.deltaTime;
        }

        if(stunCooldownCounter > 0)
        {
            stunCooldownCounter -= Time.deltaTime;
        }

        if(spawnSkeletonsCooldown > 0)
        {
            spawnSkeletonsCooldown -= Time.deltaTime;
        }

        if (transform.GetComponent<SkeletonBossMovement>().isFollowingPlayer && Vector2.Distance(attackPoint.position, transform.GetComponent<SkeletonBossMovement>().target.position) <= attackRange && attackCooldown <= 0)
        {
            attackCooldown = attackSpeed;
            Attack();
        }

        if(isEngaged && spawnSkeletonsCooldown <= 0)
        {
            SpawnSkeletons();
            spawnSkeletonsCooldown = spawnSkeletons;
        }

        if(isEngaged && Vector2.Distance(transform.position, transform.GetComponent<SkeletonBossMovement>().target.position) >= distanceToStun && slowCooldownCounter <= 0)
        {
            GameObject stun = Instantiate(stunPrefab, transform.GetComponent<SkeletonBossMovement>().target.GetComponent<PlayerController>().feetPos.position, transform.GetComponent<SkeletonBossMovement>().target.rotation);
            stun.GetComponent<Freeze>().player = transform.GetComponent<SkeletonBossMovement>().target;
            if(stunCooldownCounter <= 0)
            {
                stun.GetComponent<Freeze>().isStun = true;
                stunCooldownCounter = stunCooldown;
            }
            slowCooldownCounter = slowCooldown;
        }
    }

    public void Attack()
    {
        if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > 0 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < 45)
        {
            animator.SetTrigger("AttackRight");
        }
        else if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > 45 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < 135)
        {
            animator.SetTrigger("AttackUp");
        }
        else if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > 135 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < 180)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > -180 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < -135)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > -135 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < -45)
        {
            animator.SetTrigger("AttackDown");
        }
        else if (transform.GetComponent<SkeletonBossMovement>().angleOriginal > -45 && transform.GetComponent<SkeletonBossMovement>().angleOriginal < 0)
        {
            animator.SetTrigger("AttackRight");
        }

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerController>().TakeDamage(20);
        }
    }

    void SpawnSkeletons()
    {
        GameObject skeleton =  Instantiate(skeletonPrefab, transform.position, transform.rotation);
        skeleton.GetComponent<SkeletonMovement>().isSpawnedByBoss = true;
    }
}
