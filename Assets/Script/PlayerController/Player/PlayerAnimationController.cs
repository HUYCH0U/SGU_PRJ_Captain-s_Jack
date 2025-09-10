using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerAttack attack;
    private PlayerJump jump;
    public PlayerSwordControl sword;
    public PlayerMovingLeftRight move;
    private PlayerHealthController health;
    private PlatformCheck check;
    private Rigidbody2D rb;
    private Animator anim;
    private Animator swordVfx;
    private int switchComboAttack;
    private bool InAttackAnimation;
    private bool canPlayDeathAnim;
    private bool isSwordVfxPlaying;
    private const string attack1 = "attack";
    private const string attack2 = "attack1";
    private const string attack3 = "attack2";
    private const string throwsword = "throwsword";
    private const string idle = "idle";
    private const string run = "run";
    private const string jumpp = "jump";
    private const string fall = "fall";
    private const string hit = "hit";
    private const string death = "death";
    private const string deathground = "deathground";
    private const string appear = "appear";
    private const string haveSword = "WithSword";
    private const string noSword = "";
    private string isHaveSword = "";
    private Dialogue dialogue;
    public bool isPlayerRespawn;

    void Start()
    {
        dialogue = GetComponentInChildren<Dialogue>();
        swordVfx = GameObject.FindGameObjectWithTag("swordVfx").GetComponent<Animator>();
        move = GetComponent<PlayerMovingLeftRight>();
        jump = GetComponent<PlayerJump>();
        check = GetComponent<PlatformCheck>();
        sword = GetComponent<PlayerSwordControl>();
        attack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        switchComboAttack = 0;
        isHaveSword = haveSword;
        isSwordVfxPlaying = false;
        canPlayDeathAnim = false;
        isPlayerRespawn = false;
        health = GetComponent<PlayerHealthController>();
    }

    public IEnumerator PlayAppearAnim()
    {
        PlayAnim(appear);
        yield return new WaitForSeconds(0.5f);
        PlayAnim(idle + isHaveSword);
        yield return new WaitForSeconds(0.5f);
        isPlayerRespawn = false;
        health.isPlayerDeath = false;
        move.canMove = true;
    }


    void Update()
    {
        if (isPlayerRespawn)
            return;
        if (!move.canMove)
        {
            PlayAnim(idle);
            return;
        }
        if (health.isTakingdame || health.isPlayerDeath)
        {
            swordVfx.Play("indle");
        }

        if (health.isPlayerDeath)
        {
            if (!health.FallDeath)
                move.ReleasePosition();
            Invoke("resetcanPlayDeathAnim", 0.05f);
            if (canPlayDeathAnim)
            {
                if (!check.PlayerCheckGround(jump.RayLength))
                    PlayAnim(death);
                else
                {
                    rb.velocity = Vector3.zero;
                    PlayAnim(deathground);
                    PlayerUI.instance.disable();
                    dialogue.resetDialogue();
                    dialogue.PlayDialogueOnce(Dialogue.Death);
                    if (!isPlayerRespawn)
                    {
                        isPlayerRespawn = true;
                        Invoke("onDeathEvent", 5);
                    }
                }
                return;
            }
        }
        else if (health.isTakingdame)
        {
            attack.isAttack = false;
            PlayAnim(hit + isHaveSword);
            return;
        }
        else
        {
            if (!attack.isAttack)
            {
                swordVfx.Play("indle");
                isSwordVfxPlaying = false;
            }
            if (sword.IsHaveSword)
                isHaveSword = haveSword;
            else
                isHaveSword = noSword;

            if (!check.PlayerCheckGround(jump.RayLength))
            {
                jump.IsPlayerGround = false;
                if (rb.velocity.y > 0.1f)
                {
                    PlayAnim(jumpp + isHaveSword);
                }
                else
                    PlayAnim(fall + isHaveSword);

            }
            else
            {
                if (attack.isThrowingSword)
                {
                    PlayAnim(throwsword);
                }
                else if (attack.isAttack)
                {
                    if (switchComboAttack > 2)
                        switchComboAttack = 0;
                    if (!InAttackAnimation)
                    {
                        InAttackAnimation = true;
                        Invoke("resetInAttackAnimation", 0.4f);
                        if (!isSwordVfxPlaying)
                        {
                            isSwordVfxPlaying = true;
                            switch (switchComboAttack)
                            {
                                case 0:
                                    PlayAnim(attack1);
                                    swordVfx.Play(attack1);
                                    break;
                                case 1:
                                    PlayAnim(attack2);
                                    swordVfx.Play(attack2);

                                    break;
                                case 2:
                                    PlayAnim(attack3);
                                    swordVfx.Play(attack3);

                                    break;
                            }
                            switchComboAttack++;
                        }
                    }
                }

                else if (move.horizontal != 0)
                {
                    PlayAnim(run + isHaveSword);
                    AudioManager.Instance.PlayFootStep();
                }
                else
                {
                    PlayAnim(idle + isHaveSword);

                }
            }
        }
    }

    public void PlayAnim(string animName)
    {
        anim.Play(animName);
    }

    private void resetInAttackAnimation()
    {
        InAttackAnimation = false;
    }
    private void resetIsPlayerGround()
    {
        jump.IsPlayerGround = true;
    }
    private void resetcanPlayDeathAnim()
    {
        canPlayDeathAnim = true;
    }



    public void onDeathEvent()
    {
        Operator.Instance.DeathEvent();
    }

}
