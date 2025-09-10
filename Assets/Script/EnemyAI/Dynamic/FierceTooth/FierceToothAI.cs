using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierceToothAI : MonoBehaviour
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
    private float CountDown;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        isDetectPlayer = false;
        CountDown = -1;

    }

    void Update()
    {
        if (CountDown > 0)
            CountDown -= Time.deltaTime;
        if (health.isTakingDame || health.isDeath)
        {
            if (!isDetectPlayer)
                isDetectPlayer = true;
            anim.isAttack = false;
            anim.HealthAnimation();
            return;
        }
        if (anim.isJumping)
        {
            anim.baseAnimation(false);
            return;
        }

        if (anim.isAttack || !check.CheckGround(0.1f))
            return;
        if (checkplayer.CheckPlayerInBack(ActiveRange) && !isDetectPlayer)
        {
            isDetectPlayer = true;
            anim.StartFoundPlayerDialogue();
            anim.StartFoundPlayer(1);
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
        
        if (Vector2.Distance(player.transform.position, this.transform.position) < ActiveRange && CountDown < 0
         || checkplayer.heightDiff(player) && Vector2.Distance(player.transform.position, this.transform.position) < 4)
        {
            if (check.CheckGroundInFront(0.5f))
            anim.StartJumping(0.4f, 0.3f, 12);
            else{
            CountDown = 3f;
            anim.StartBaseAndDashAttack(1.75f, 0.25f, 10);
            }
        }
        else
        {
            flip.LookAtPlayer();
            if (!isRuning)
                isRuning = true;
            anim.baseAnimation(isRuning);
            if (check.CheckGroundInFront(0.5f))
            anim.StartJumping(0.4f, 0.3f, 13);
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newposition = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);

            rb.MovePosition(newposition);
        }
    }
}
