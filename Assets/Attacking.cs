using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public static Attacking instance;

    void Awake()
    {
        instance = this;
    }

    public Transform attackPoint;

    public float damage = 20;

    public float attackCooldown;
    float attackCooldownCounter;

    public GameObject CrossedSpellPrefab;
    public bool CanUseCrossedSpell = true;
    public float spellSpeed;

    public GameObject BoltPrefab;
    public bool CanUseBoltSpell = true;

    public GameObject wavePrefab;
    public bool CanUseWaveSpell = true;

    public GameObject arrowPrefab;
    public float arrowSpeed;
    public float shootCooldown;
    float shootCooldownCounter;

    //public GameObject spellPrefab;

    public GameObject[] skillSlots;

    public Rigidbody2D firePointRb;
    public Camera cam;
    public float angleOriginal;
    Vector2 mousePosition;

    public float attackRange;
    public LayerMask enemyLayers;

    public LayerMask resourcesToBeGathered;

    public Animator animator;
    // Update is called once per frame
    void Update()
    {

        if(attackCooldownCounter > 0)
        {
            attackCooldownCounter -= Time.deltaTime;
        }

        if (shootCooldownCounter > 0)
        {
            shootCooldownCounter -= Time.deltaTime;
        }

        if (attackPoint.position != transform.position)
        {
            attackPoint.position = transform.position;
        }

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePosition - firePointRb.position;
        angleOriginal = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePointRb.rotation = angle;

        if (Input.GetButtonDown("Fire2"))
        {
            if (Inventory.instance.currentEquipment[7] != null && Inventory.instance.currentEquipment[7].IsRanged == false && attackCooldownCounter <= 0)
            {
                Attack();
                attackCooldownCounter = attackCooldown - PlayerController.instance.Agility/50;
            }else if(Inventory.instance.currentEquipment[7] != null && Inventory.instance.currentEquipment[7].IsRanged == true && shootCooldownCounter <= 0)
            {
                Shoot("Arrow");
                shootCooldownCounter = shootCooldown - PlayerController.instance.Agility / 50;
            }
        }
        if (Input.GetKey("1") && CanUseCrossedSpell)
        {
            Shoot("CrossedShot");
        }
        if (Input.GetKey("2") && CanUseBoltSpell)
        {
            Shoot("Bolt");
        }
        if (Input.GetKey("3") && CanUseWaveSpell)
        {
            Shoot("Wave");
        }
    }

    void Shoot(string type)
    {
        if(type == "Arrow")
        {
            if (angleOriginal > 0 && angleOriginal < 45)
            {
                animator.SetTrigger("ShootRight");
            }
            else if (angleOriginal > 45 && angleOriginal < 135)
            {
                animator.SetTrigger("ShootUp");
            }
            else if (angleOriginal > 135 && angleOriginal < 180)
            {
                animator.SetTrigger("ShootLeft");
            }
            else if (angleOriginal > -180 && angleOriginal < -135)
            {
                animator.SetTrigger("ShootLeft");
            }
            else if (angleOriginal > -135 && angleOriginal < -45)
            {
                animator.SetTrigger("ShootDown");
            }
            else if (angleOriginal > -45 && angleOriginal < 0)
            {
                animator.SetTrigger("ShootRight");
            }

            GameObject arrow = Instantiate(arrowPrefab, attackPoint.position, attackPoint.rotation);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.AddForce(attackPoint.up * arrowSpeed, ForceMode2D.Impulse);
        }

        if (type == "CrossedShot")
        {
            if (PlayerController.instance.UseMana(10))
            {
                GameObject crossedSpell = Instantiate(CrossedSpellPrefab, attackPoint.position, attackPoint.rotation);
                Rigidbody2D rb = crossedSpell.GetComponent<Rigidbody2D>();
                rb.AddForce(attackPoint.up * spellSpeed, ForceMode2D.Impulse);
                crossedSpell.GetComponent<Spell>().damage += PlayerController.instance.Intelect;
                CanUseCrossedSpell = false;
            }


        }
        if (type == "Bolt")
        {
            if (PlayerController.instance.UseMana(10))
            {
                GameObject boltSpell = Instantiate(BoltPrefab, attackPoint.position, attackPoint.rotation);
                Rigidbody2D rb = boltSpell.GetComponent<Rigidbody2D>();
                rb.AddForce(attackPoint.up * spellSpeed, ForceMode2D.Impulse);
                boltSpell.GetComponent<Spell>().damage += PlayerController.instance.Intelect;
                CanUseBoltSpell = false;
            }


        }
        if (type == "Wave")
        {
            if (PlayerController.instance.UseMana(10))
            {
                GameObject waveSpell = Instantiate(wavePrefab, attackPoint.position, attackPoint.rotation);
                Rigidbody2D rb = waveSpell.GetComponent<Rigidbody2D>();
                rb.AddForce(attackPoint.up * spellSpeed, ForceMode2D.Impulse);
                waveSpell.GetComponent<Spell>().damage += PlayerController.instance.Intelect;
                CanUseWaveSpell = false;
            }


        }

        foreach (GameObject skillSlot in skillSlots)
        {
            if(skillSlot.GetComponent<CooldownCounter>().Skill == type)
            {
                skillSlot.GetComponent<CooldownCounter>().PutOnCoolDown(2);
            }
        }
    }

    void Attack()
    {
        if (angleOriginal > 0 && angleOriginal < 45)
        {
            animator.SetTrigger("AttackRight");
        }
        else if (angleOriginal > 45 && angleOriginal < 135)
        {
            animator.SetTrigger("AttackUp");
        }
        else if (angleOriginal > 135 && angleOriginal < 180)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (angleOriginal > -180 && angleOriginal < -135)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (angleOriginal > -135 && angleOriginal < -45)
        {
            animator.SetTrigger("AttackDown");
        }
        else if (angleOriginal > -45 && angleOriginal < 0)
        {
            animator.SetTrigger("AttackRight");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitRsources = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, resourcesToBeGathered);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage + PlayerController.instance.Strength);
        }

        foreach (Collider2D resource in hitRsources)
        {
            resource.GetComponent<AbleToBeGathered>().Gather();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
