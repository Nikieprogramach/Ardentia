using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed;
    public Transform target;
    private bool playerInRadius;
    public float checkRadiusForPlayer;
    public LayerMask player;
    public bool isFollowingPlayer;

    public bool isSpawnedByBoss = false;

    public Enemy Enemy;

    public Animator animator;

    public Camera cam;
    Vector2 mousePosition;
    public float angleOriginal;

    public void Start()
    {
        Init();
    }

    void Init()
    {
        Enemy = transform.GetComponent<Enemy>();
        cam = PlayerController.instance.cam.GetComponent<Camera>();
    }

    void Update()
    {
        playerInRadius = Physics2D.OverlapCircle(transform.position, checkRadiusForPlayer, player);

        if (playerInRadius || isSpawnedByBoss)
        {
            isFollowingPlayer = true;
            target = Physics2D.OverlapCircle(transform.position, 100, player).GetComponent<Transform>();
        }

        if (isFollowingPlayer == true && Vector2.Distance(transform.GetComponent<EnemyAttack>().attackPoint.position, target.position) > transform.GetComponent<EnemyAttack>().attackRange)
        {
            animator.SetBool("IsMoving", true);

            Vector2 lookDir = target.GetComponent<Rigidbody2D>().position - transform.GetComponent<Rigidbody2D>().position;
            angleOriginal = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (angleOriginal > 0 && angleOriginal < 45)
            {
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            else if (angleOriginal > 45 && angleOriginal < 135)
            {
                animator.SetFloat("Vertical", 1);
                animator.SetFloat("Horizontal", 0);
            }
            else if (angleOriginal > 135 && angleOriginal < 180)
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
            else if (angleOriginal > -180 && angleOriginal < -135)
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
            else if (angleOriginal > -135 && angleOriginal < -45)
            {
                animator.SetFloat("Vertical", -1);
                animator.SetFloat("Horizontal", 0);
            }
            else if (angleOriginal > -45 && angleOriginal < 0)
            {
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);

            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }
}
