using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CanonAI : MonoBehaviour
{
    [SerializeField] private float ActiveRange;
    [SerializeField] private Transform SpawningPos;
    private BaseAnimation anim;
    private EnemyHeath health;
    private CheckPlayer checkplayer;
    private GameObject player;
    public float CountDown;
    private const string ObjectName = "CanonBall";
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        anim = GetComponent<BaseAnimation>();
        CountDown = -1;
    }

    void Update()
    {
        if (health.isTakingDame || health.isDeath)
        {
            anim.StaticEnemyHealthAnimation();
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

        if (checkplayer.CheckPlayerInBack(ActiveRange) && CountDown < 0)
        {
            CountDown = 2;
            anim.StartBaseStaticAttack(1.5f);
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
