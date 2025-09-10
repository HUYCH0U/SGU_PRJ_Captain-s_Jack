using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShellAI : MonoBehaviour
{
    [SerializeField] private float ActiveRangeShoot;
    [SerializeField] private float ActiveRangeBite;
    [SerializeField] private Transform SpawningPos;
    [SerializeField] private Transform BitePos;
    [SerializeField] private float BiteRange;
    private BaseAnimation anim;
    private Rigidbody2D rb;
    private EnemyHeath health;
    private CheckPlayer checkplayer;
    private GameObject player;
    public float CountDown;
    private DealingDamage deal;
    private const string ObjectName = "Pear";



    void Start()
    {
        deal = GetComponent<DealingDamage>();
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<BaseAnimation>();
        CountDown = -1;
    }

    public void Bite()
    {
        deal.EnemyAttackSquare(new Vector2(BiteRange,BiteRange), BitePos, 1);
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(BitePos.position,BiteRange);
    }
    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
            if(anim.isAttack)
            anim.isAttack = false;
            anim.StaticEnemyHealthAnimation();
            CountDown=-1;
            return;
        }
        if (anim.isAttack)
            return;
        if (player.GetComponent<PlayerHealthController>().isPlayerDeath)
        {
            anim.PlayAnim("idle");
            return;
        }
        if (CountDown > 0)
            CountDown -= Time.deltaTime;

        if (checkplayer.CheckPlayerInBack(ActiveRangeShoot))
        {
            if (Vector2.Distance(rb.transform.position, player.transform.position) <= ActiveRangeBite)
            {
                anim.StartModifyAnim(1.5f, "bite");
            }
            else if (CountDown < 0)
            {

                CountDown = 1.5f;
                anim.StartBaseStaticAttack(1.5f);
            }
            else
                return;
        }
        else
        {
            anim.PlayAnim("idle");
        }
    }

    public void Fire()
    {
        StaticEnemyObjectPooling.instance.SpawningObject(SpawningPos, ObjectName);
    }
}
