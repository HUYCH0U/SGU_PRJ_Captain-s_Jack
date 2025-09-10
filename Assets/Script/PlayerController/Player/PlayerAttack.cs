using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform AttackPos;
    [SerializeField] private float AttackRange;
    private Camera mainCam;
    private PlayerBaseState state;
    private Flip flip;
    public bool isAttack;
    private PlayerJump jump;
    private PlayerSwordControl sword;
    private PlayerMovingLeftRight move;
    public bool isThrowingSword;
    private Vector3 mousePos;
    public bool canAttack;
    private PlayerHealthController health;
    private DealingDamage dame;

    void Start()
    {
        dame=GetComponent<DealingDamage>();
        health = GetComponent<PlayerHealthController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        isThrowingSword = false;
        isAttack = false;
        canAttack=true;
        sword = GetComponent<PlayerSwordControl>();
        move = GetComponent<PlayerMovingLeftRight>();
        flip = GetComponent<Flip>();
        state = GetComponent<PlayerBaseState>();
        jump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (!jump.isGrounding || health.isPlayerDeath||!move.canMove)
            return;
        if (Input.GetMouseButtonDown(0) && !isAttack && sword.IsHaveSword&&canAttack)
        {
            canAttack=false;
            checkside();
            isAttack = true;
            flip.CanFlip = false;
            move.FreezePosition();
            Invoke("ResetAttack", 0.4f);
            Invoke("ResetCanAttack", 0.5f);
        }
        else if (Input.GetMouseButtonDown(1) && !isAttack && sword.IsHaveSword && !isThrowingSword)
        {
            sword.IsHaveSword = false;
            checkside();
            isAttack = true;
            move.FreezePosition();
            isThrowingSword = true;
            Invoke("resetisThrowingSword", 0.5f);
            Invoke("SetWayToSword", 0.25f);

        }
    }

    public void DealDamage()
    {
        dame.PlayerAttack(AttackRange,AttackPos,state.getSlashAttackDame());
    }
    public void SwordOnPlayerDeath()
    {
        if (sword.IsHaveSword)
        {
            Vector3 direction = Vector3.zero;
            if (flip.isFacingRight)
            {
                direction = new Vector3(1,2,0);
            }
            else
            {
                direction = new Vector3(-1,2,0);
            }
            sword.SpawnSword(direction,true,state.getSlashAttackDame()+1);
        }
        else
            return;
    }

    private void ResetCanAttack()
    {
        canAttack=true;
    }
    private void ResetAttack()
    {
        isAttack = false;
        flip.CanFlip = true;
        move.ReleasePosition();
    }



    private void resetisThrowingSword()
    {
        isAttack = false;
        move.ReleasePosition();
        isThrowingSword = false;
    }
    public void SetWayToSword()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - transform.position).normalized;
        sword.SpawnSword(direction,false,state.getSlashAttackDame()*2);
    }
    private void checkside()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (flip.isFacingRight && mousePos.x < transform.position.x)
        {
            move.horizontal = -1f;
            flip.PlayerFlip();
        }
        else if (!flip.isFacingRight && mousePos.x > transform.position.x)
        {
            move.horizontal = 1f;
            flip.PlayerFlip();
        }
    }


}
