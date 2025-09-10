using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishAI : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private int ActiveRange;
    private BaseAnimation anim;
    private PlatformCheck check;
    private Rigidbody2D rb;
    private Flip flip;
    public bool isRuning;
    private EnemyHeath health;
    private CheckPlayer checkplayer;
    public bool isDetectPlayer;
    private GameObject player;
    private float countDown;
    private bool isRoll;
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
        isRoll = false;
        countDown = -1;
    }

    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
            if (!isDetectPlayer)
            {
                flip.LookAtPlayer();
                isDetectPlayer = true;
            }
            anim.isAttack = false;
            isRoll = false;
            countDown = 1f;
            anim.HealthAnimation();
            return;
        }
        if (checkplayer.CheckPlayerInBack(ActiveRange) && !isDetectPlayer)
        {
            anim.StartFoundPlayerDialogue();
            isDetectPlayer = true;
        }
        if (player.GetComponent<PlayerHealthController>().isPlayerDeath || !isDetectPlayer)
        {
            anim.PlayAnim("idle");
            return;
        }
        if (isDetectPlayer)
            AttackState();

    }

    private void AttackState()
    {

        if (countDown > 0)
        {
            anim.PlayAnim("idle");
            countDown -= Time.deltaTime;
        }
        else
        {
            anim.PlayAnim("attack");
            if (isRoll)
            {
                StartRoll();
            }
        }
    }

    private void StartRoll()
    {
        if (isRoll && check.CheckGroundInFront(0.65f) || checkplayer.CheckPlayerInBack(0.5f))
        {
            rb.velocity=Vector2.zero;
            isRoll = false;
            flip.LookAtPlayer();
            countDown = 2f;
        }
        else if (!check.CheckGroundInFront(0.65f) || !checkplayer.CheckPlayerInBack(0.5f))
        {
            rb.velocity = new Vector2(flip.getSide() * Speed, rb.velocity.y);
        }
    }

    public void Roll()
    {

        flip.LookAtPlayer();
        isRoll = true;
    }

    public void checkPlayer()
    {
        if (check.CheckGroundInFront(0.65f))
        {
            isDetectPlayer = false;
        }
    }
}
