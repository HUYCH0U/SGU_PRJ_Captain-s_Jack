using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAI : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private int KeepdistanceRange;
    private string BulletName = "WoodBullet";
    private BaseAnimation anim;
    private PlatformCheck check;
    private Rigidbody2D rb;
    private Flip flip;
    public bool isRuning;
    private CheckPlayer checkplayer;
    private GameObject player;
    private EnemyHeath health;
    private bool isDetectPlayer;
    private Transform SpawningPos;
    void Start()
    {
        SpawningPos=transform.Find("ShootPos");
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        isDetectPlayer = false;
    }

    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
            anim.HealthAnimation();
            if (!isDetectPlayer)
                isDetectPlayer = true;
            anim.isAttack = false;
            return;
        }

        if (anim.isAttack || !check.CheckGround(0.1f) || anim.isJumping)
            return;
        if (checkplayer.CheckPlayerInBack(15f) && !isDetectPlayer)
        {
            isDetectPlayer = true;
            anim.StartFoundPlayer(0.5f);
            anim.StartFoundPlayerDialogue();

        }
        else if (isDetectPlayer && checkplayer.DistanceCheck(player, 15))
        {
            isDetectPlayer = false;
        }
        if (player.GetComponent<PlayerHealthController>().isPlayerDeath || !isDetectPlayer)
        {
            anim.PlayAnim("idle");
            return;
        }
        if (anim.isFoundPlayer)
            return;

        if (isDetectPlayer)
        {
            if (check.CheckLeftRight(1f))
                AttackState();
            else
                KeepDistanceState();
        }
    }

    private void AttackState()
    {
        flip.LookAtPlayer();
        anim.StartBaseAttack(1);
        anim.StartFoundPlayer(1);
    }
    private void KeepDistanceState()
    {
        flip.NotLookAtPlayer();
        if (!isRuning)
            isRuning = true;
        anim.baseAnimation(isRuning);
        rb.velocity = new Vector2(checkplayer.GetPlayerSide(player) * Speed, rb.velocity.y);
    }

    public void Fire()
    {
        StaticEnemyObjectPooling.instance.SpawningObject(SpawningPos, BulletName);
    }
}
