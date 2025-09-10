using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainBomb : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float Range;
    private Animator anim;
    private Rigidbody2D rb;
    private DealingDamage deal;
    private PlatformCheck check;
    private GameObject Player;
    void Start()
    {
        check = GetComponent<PlatformCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deal = GetComponent<DealingDamage>();
        Player=GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.GetComponent<Collider2D>(), true);
        if (check.CheckGround(0.5f))
        {
            anim.Play("explose");
        }
    }

    public void checkplayer()
    {
        deal.EnemyAttackCircle(Range, transform, 1);
    }

    public void destroy()
    {
        gameObject.SetActive(false);
    }
}
