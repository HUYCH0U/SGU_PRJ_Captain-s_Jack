using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BaseAnimation : MonoBehaviour
{
    private Animator anim;
    private PlatformCheck check;
    private EnemyHeath health;
    private bool canCheck;
    private const string run = "run";
    private const string idle = "idle";
    private const string takingdame = "takingdame";
    private const string death = "death";
    private const string deathground = "deathground";
    private const string attack = "attack";
    public bool GroundBreak = true;
    private Flip flip;
    public bool isAttack;
    public bool isFoundPlayer;
    private BreakableObject breakobject;
    private Rigidbody2D rb;
    public bool isJumping;
    private bool isDeath;
    private Dialogue dialogue;
    private Collider2D player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        dialogue = GetComponentInChildren<Dialogue>();
        rb = GetComponent<Rigidbody2D>();
        flip = GetComponent<Flip>();
        anim = GetComponent<Animator>();
        check = GetComponent<PlatformCheck>();
        health = GetComponent<EnemyHeath>();
        breakobject = GetComponent<BreakableObject>();
        canCheck = false;
        isAttack = false;
        isFoundPlayer = false;
        isJumping = false;
        isDeath = false;
    }

    public void Disappear()
    {
        DisableCollider();
        isDeath = true;
    }

    private void DisableCollider()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.GetComponent<Collider2D>().enabled = false;
        Invoke("destroy", 2);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void baseAnimation(bool isRuning)
    {
        if (isJumping)
        {
            if (rb.velocity.y > 0.1f)
                PlayAnim("jump");
            else
                PlayAnim("fall");
        }
        else
        if (isRuning)
        {
            anim.Play(run);
        }
        else
            anim.Play(idle);

    }

    public void PlayAnim(string Anim)
    {
        anim.Play(Anim);
    }

    public void HealthAnimation()
    {
        if (health.isDeath)
        {
            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), player, true);
            Invoke("resetCanCheck", 0.05f);
            if (canCheck)
            {
                if (!check.CheckGround(0.1f) && !isDeath)
                    anim.Play(death);
                else
                {
                    anim.Play(deathground);
                    dialogue.PlayDialogueOnce(Dialogue.Death);
                }
            }
        }
        else if (health.isTakingDame)
        {
            anim.Play(takingdame);
        }
    }
    public void StaticEnemyHealthAnimation()
    {
        if (health.isDeath)
        {
            Invoke("resetCanCheck", 0.05f);
            if (canCheck)
            {
                if (GroundBreak)
                {

                    if (!check.CheckGround(0.1f))
                        anim.Play(death);
                    else
                    {
                        breakobject.Break(true);
                    }
                }
                else
                    anim.Play(death);
            }
        }
        else if (health.isTakingDame)
        {
            anim.Play(takingdame);
        }
    }

    public void StaticEnemyBreak()
    {
        breakobject.Break(true);
    }

    private void resetCanCheck()
    {
        canCheck = true;
    }

    private IEnumerator BaseAttack(float Casttime)
    {
        flip.LookAtPlayer();
        isAttack = true;
        anim.Play(attack);
        yield return new WaitForSeconds(Casttime);
        isAttack = false;
    }
    private IEnumerator BaseStaticModifyAnim(float Casttime, string animname)
    {
        isAttack = true;
        anim.Play(animname);
        yield return new WaitForSeconds(Casttime);
        isAttack = false;
    }
    private IEnumerator Jump(float forwardtime, float uptime, float force)
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, force);
        yield return new WaitForSeconds(uptime);
        rb.velocity = new Vector2(flip.getSide() * 5, rb.velocity.y);
        yield return new WaitForSeconds(forwardtime);
        isJumping = false;
    }
    private IEnumerator BaseAttackAndDash(float Casttime, float dashtime, float force)
    {
        flip.LookAtPlayer();
        isAttack = true;
        anim.Play(attack);
        yield return new WaitForSeconds(dashtime);
        if (!health.isTakingDame)
        {
            rb.velocity = new Vector2(flip.getSide() * force, rb.velocity.y);
            yield return new WaitForSeconds(Casttime);
            isAttack = false;
        }
        else
        {
            isAttack = false;
            yield break;
        }
    }
    private IEnumerator BaseStaticAttack(float Casttime)
    {
        isAttack = true;
        anim.Play(attack);
        yield return new WaitForSeconds(Casttime);
        isAttack = false;
    }

    private IEnumerator FoundPlayer(float DelayTime)
    {
        isFoundPlayer = true;
        anim.Play(idle);
        yield return new WaitForSeconds(DelayTime);
        isFoundPlayer = false;
    }

    public void StartFoundPlayerDialogue()
    {
        dialogue.PlayDialogueOnce(Dialogue.ExcalamationMark);
        dialogue.resetDialogue();
    }

    public void PlayModifiDialouge(string dialogueString)
    {
        dialogue.PlayDialogueOnce(dialogueString);
        dialogue.resetDialogue();
    }
    public void StartJumping(float uptime, float forwardtime, float force)
    {
        StartCoroutine(Jump(uptime, forwardtime, force));
    }

    public void StartBaseAttack(float Casttime)
    {
        StartCoroutine(BaseAttack(Casttime));
    }
    public void StartModifyAnim(float Casttime, string animname)
    {
        StartCoroutine(BaseStaticModifyAnim(Casttime, animname));
    }

    public void StartBaseAndDashAttack(float Casttime, float dashtime, float Force)
    {
        StartCoroutine(BaseAttackAndDash(Casttime, dashtime, Force));
    }
    public void StartBaseStaticAttack(float Casttime)
    {
        StartCoroutine(BaseStaticAttack(Casttime));
    }

    public void StartFoundPlayer(float DelayTime)
    {
        StartCoroutine(FoundPlayer(DelayTime));
    }
}
