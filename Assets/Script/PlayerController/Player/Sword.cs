using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] private float Range;
    public Vector3 way;
    public int Damage;
    private Animator anim;
    private Rigidbody2D rb;
    private bool CanCheckPlayer;
    private PlatformCheck check;
    public bool isPlayerDeath;
    public bool isDealDamage;
    private DealingDamage dame;
    private GameObject[] enemy;

    void Start()
    {
        check = GetComponent<PlatformCheck>();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        dame = GetComponent<DealingDamage>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.velocity = way.normalized * speed;
        CanCheckPlayer = false;
        isDealDamage = false;
        Invoke("canCheck", 0.05f);
    }

    public void canCheck()
    {
        CanCheckPlayer = true;
    }

    private void ignoreEnemy()
    {
        foreach (GameObject enemy in enemy)
            if (enemy != null)
            {
                Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), true);
            }
    }

    void Update()
    {
        if (isPlayerDeath)
        {
            ignoreEnemy();
            anim.Play("static");
        }
        if (check.CheckGround(0.05f))
        {
            ignoreEnemy();
            if (!isDealDamage)
                isDealDamage = true;
            zero();
        }
        if (isDealDamage)
        {
            ignoreEnemy();
        }
        else
            return;
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isPlayerDeath)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                anim.Play("static");
            }
            if (other.gameObject.CompareTag("Player") && CanCheckPlayer)
            {
                PlayerSwordControl sword = other.gameObject.GetComponent<PlayerSwordControl>();
                sword.IsHaveSword = true;
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("ItemContainObject"))
            {
                if (!isDealDamage)
                {
                    isDealDamage = true;
                    dame.PlayerThowingSwordAttack(Range, transform, Damage);
                }
            }
        }
    }

    private void zero()
    {
        rb.velocity = Vector3.zero;

    }

}
