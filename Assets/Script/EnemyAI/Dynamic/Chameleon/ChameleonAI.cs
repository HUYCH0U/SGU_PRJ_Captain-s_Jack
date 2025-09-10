using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonAI : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private float ActiveRange;
    private BaseAnimation anim;
    private PlatformCheck check;
    private Rigidbody2D rb;
    private Flip flip;
    public bool isRuning;
    private CheckPlayer checkplayer;
    private GameObject player;
    private EnemyHeath health;
    private bool isDetectPlayer;
    private DealingDamage dame;
    public Transform attackPos;
    [SerializeField] private float AttackRange;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        dame = GetComponent<DealingDamage>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        isDetectPlayer = false;

    }

    public void Attack()
    {
        dame.EnemyAttackSquare(new Vector2(AttackRange + 3, AttackRange), attackPos, 2);
    }


    // void OnDrawGizmos()
    // {
    // Gizmos.DrawWireCube(attackPos.transform.position, new Vector2(AttackRange+3,AttackRange));
    // }

    void Update()
    {

        if (health.isTakingDame || health.isDeath)
        {
            if (!isDetectPlayer)
                isDetectPlayer = true;
            anim.isAttack = false;
            anim.HealthAnimation();
            return;
        }

        if (anim.isAttack || !check.CheckGround(0.1f)||anim.isJumping)
            return;
        if (checkplayer.CheckPlayerInBack(15f) && !isDetectPlayer)
        {
            anim.StartFoundPlayer(1f);
            anim.StartFoundPlayerDialogue();
            isDetectPlayer = true;
        }
        else if (isDetectPlayer && checkplayer.DistanceCheck(player, 15))
        {
            isDetectPlayer = false;
        }

        if(anim.isFoundPlayer)
            return;

        if (player.GetComponent<PlayerHealthController>().isPlayerDeath || !isDetectPlayer)
        {
            anim.PlayAnim("idle");
            return;
        }

        if (isDetectPlayer)
            ChaseState();
        

    }

    private void ChaseState()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) <= ActiveRange
         || checkplayer.heightDiff(player) && Vector2.Distance(player.transform.position, this.transform.position) < 4)
        {
            if (check.CheckGroundInFront(0.75f))
                anim.StartJumping(0.4f, 0.3f, 12);
            else
            {
                anim.StartBaseAttack(1.75f);
            }
        }
        else
        {
            flip.LookAtPlayer();
            if (!isRuning)
                isRuning = true;
            anim.baseAnimation(isRuning);
            if (check.CheckGroundInFront(0.75f))
                anim.StartJumping(0.4f, 0.3f, 13);
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newposition = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);
            rb.MovePosition(newposition);
        }
    }
}
