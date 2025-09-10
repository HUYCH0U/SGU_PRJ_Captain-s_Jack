using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAI : MonoBehaviour
{
    private bool IsSpikeOut;
    private bool isDetectPlayer;
    private BaseAnimation anim;
    private Flip flip;
    private CheckPlayer check;
    private EnemyHeath health;
    private float CountDown;
    private GameObject player;
    private Animator animator;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHeath>();
        check = GetComponent<CheckPlayer>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        IsSpikeOut = false;
        isDetectPlayer = false;
        CountDown = 1;
    }

    void Update()
    {
        if (CountDown >= 0)
            CountDown -= Time.deltaTime;
        if (health.isTakingDame || health.isDeath)
        {
            anim.HealthAnimation();
            return;
        }
        else
        if (Vector2.Distance(transform.position, player.transform.position) < 15f)
        {
            if (!isDetectPlayer)
            {
                isDetectPlayer = true;
                anim.StartFoundPlayerDialogue();
            }
            flip.LookAtPlayer();
        }
        else
        {
            isDetectPlayer = false;
            if (IsSpikeOut)
            {
                IsSpikeOut = false;
                StartCoroutine(spikeOff());
            }
            else
            {
                anim.PlayAnim("idle");
            }
        }
        if (isDetectPlayer)
        {

            if (CountDown < 0 && !IsSpikeOut)
            {
                CountDown = 2f;
                IsSpikeOut = true;
                StartCoroutine(spikeOut());
            }
            else if (CountDown < 0 && IsSpikeOut)
            {
                CountDown = 3f;
                IsSpikeOut = false;
                StartCoroutine(spikeOff());
            }
        }

    }

    private IEnumerator spikeOut()
    {
        anim.PlayAnim("spikeon");
        health.canTakeDame = false;
        yield return new WaitForSeconds(0.5f);
        anim.PlayAnim("idlespike");

    }

    private IEnumerator spikeOff()
    {
        anim.PlayAnim("spikeoff");
        yield return new WaitForSeconds(0.5f);
        anim.PlayAnim("idle");
        health.canTakeDame = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsSpikeOut)
            {
                PlayerHealthController.instance.TakingDame(1, transform.position);
            }
        }
    }
}
