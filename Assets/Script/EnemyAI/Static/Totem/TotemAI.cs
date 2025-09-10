using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAI : MonoBehaviour
{
    [SerializeField] private float ActiveRange;
    [SerializeField] private Transform SpawningPos;
    private BaseAnimation anim;
    private Rigidbody2D rb;
    private EnemyHeath health;
    private CheckPlayer checkplayer;
    private GameObject player;
    private float CountDown;
    private const string ObjectName = "Spike";
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
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

        if (CountDown < 0)
        {
            if (checkplayer.CheckPlayerInBack(ActiveRange))
            {
                CountDown = 3;
                anim.StartBaseStaticAttack(1.5f);
            }
            else
            {
                anim.PlayAnim("idle");
            }
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
