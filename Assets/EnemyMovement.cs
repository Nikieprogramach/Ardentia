using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform target;
    private bool playerInRadius;
    public float checkRadiusForPlayer;
    public LayerMask player;
    public bool isFollowingPlayer;

    public Enemy Enemy;

    public Animator animator;

    public void Start()
    {
        Init();
    }

    void Init()
    {
        Enemy = transform.GetComponent<Enemy>();
    }

    void Update()
    {
        playerInRadius = Physics2D.OverlapCircle(transform.position, checkRadiusForPlayer, player);

        if (playerInRadius)
        {
            isFollowingPlayer = true;
            target = Physics2D.OverlapCircle(transform.position, 100, player).GetComponent<Transform>();
        }

        if (isFollowingPlayer == true)
        {
            //animator.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
