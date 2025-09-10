using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour
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
    private bool IsTouchWall;
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
        IsTouchWall = false;
    }

    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
      
            if (!isDetectPlayer)
                isDetectPlayer = true;
            anim.HealthAnimation();
            return;
        }

        if (checkplayer.CheckPlayerInBack(ActiveRange) && !isDetectPlayer)
        {
            anim.StartFoundPlayer(1f);
            anim.StartFoundPlayerDialogue();
            isDetectPlayer = true;
        }
        else if (isDetectPlayer && checkplayer.DistanceCheck(player, 25))
        {
            isDetectPlayer = false;
        }
        if (anim.isFoundPlayer || !check.CheckGround(0.1f))
            return;
        if (isDetectPlayer)
            AttackState();
        else
        {
            anim.PlayAnim("idle");
        }
    }

    private void AttackState()
    {
        if (check.CheckLeftRight(1f) && !IsTouchWall)
        {
            IsTouchWall = true;
            anim.PlayAnim("idle");
            flip.LookAtPlayer();
            Invoke("resetTouchWall", 2f);
            anim.StartFoundPlayer(1f);
        }
        else
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < 3)
            {
                anim.StartBaseStaticAttack(0.5f);
            }
            else
            {
                if (!isRuning)
                    isRuning = true;
                anim.baseAnimation(isRuning);
            }
            rb.velocity = -transform.right * Speed;
        }
    }



    private void resetTouchWall()
    {
        IsTouchWall = false;
    }
}
