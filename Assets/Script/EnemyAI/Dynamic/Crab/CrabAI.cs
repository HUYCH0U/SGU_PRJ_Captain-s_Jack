using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CrabAI : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private int Damage;
    [SerializeField] private float ActiveRange;
    [SerializeField] private float AttackRange;
    public Transform AttackPos1;
    public Transform AttackPos2;
    private BaseAnimation anim;
    private PlatformCheck check;
    private NoncombatMove non;
    private Rigidbody2D rb;
    private Flip flip;
    public bool isRuning;
    private EnemyHeath health;
    private CheckPlayer checkplayer;
    private bool isDetectPlayer;
    private GameObject player;
    private DealingDamage damage;





    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = GetComponent<DealingDamage>();
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        non = GetComponent<NoncombatMove>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        isDetectPlayer = false;
    }

    public void AttackMethod()
    {
        damage.EnemyAttackCircle(AttackRange, AttackPos1, Damage);
        damage.EnemyAttackCircle(AttackRange, AttackPos2, Damage);
    }
    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
            if (anim.isAttack)
                anim.isAttack = false;
            if (anim.isFoundPlayer)
                anim.isFoundPlayer = false;
            anim.HealthAnimation();
            return;
        }
        if (player.GetComponent<PlayerHealthController>().isPlayerDeath)
        {
            anim.PlayAnim("idle");
            return;
        }
        if (anim.isJumping)
        {
            anim.baseAnimation(false);
            return;
        }
        if (anim.isAttack || anim.isFoundPlayer || !check.CheckGround(0.1f))
            return;
        if (checkplayer.CheckPlayerInFront(10f) && !isDetectPlayer)
        {
            flip.ResetFlip();
            anim.StartFoundPlayer(2f);
            anim.StartFoundPlayerDialogue();
            isDetectPlayer = true;
        }
        else if (isDetectPlayer && checkplayer.DistanceCheck(player, 15))
        {
            anim.StartFoundPlayer(2f);
            isDetectPlayer = false;
        }

        anim.baseAnimation(isRuning);
        if (isDetectPlayer)
            ChaseState();
        else
            NormalState();
    }

    private void NormalState()
    {
        if (!isRuning)
            isRuning = true;
        non.GoLeftAndRight(Speed);

    }

    private void ChaseState()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < ActiveRange || checkplayer.heightDiff(player) && Vector2.Distance(player.transform.position, this.transform.position) < 3.5)
        {
            if (check.CheckGroundInBack(0.75f))
                anim.StartJumping(0.4f, 0.3f, 14);
            else
                anim.StartBaseAttack(1.25f);
        }
        else
        {
            flip.LookAtPlayer();
            if (!isRuning)
                isRuning = true;
            if (check.CheckGroundInBack(0.75f))
                anim.StartJumping(0.4f, 0.3f, 14);
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newposition = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);
            rb.MovePosition(newposition);
        }
    }

    //    void OnDrawGizmos()
    //     {
    //         Gizmos.DrawWireSphere(AttackPos1.position,AttackRange);
    //         Gizmos.DrawWireSphere(AttackPos2.position,AttackRange);
    //     }

}
