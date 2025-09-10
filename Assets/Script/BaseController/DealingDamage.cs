using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealingDamage : MonoBehaviour
{
    private LayerMask Player;
    private LayerMask Enemy;
    private LayerMask ContainItem;

    void Start()
    {
        Player = LayerMask.GetMask("Player");
        Enemy = LayerMask.GetMask("Enemy");
        ContainItem = LayerMask.GetMask("ItemContainObject");
    }

    public void EnemyAttackCircle(float AttackRange, Transform AttackHitBox, int AttackDame)
    {
        Collider2D HitInfo = Physics2D.OverlapCircle(AttackHitBox.position, AttackRange, Player);
        if (HitInfo != null)
        {
            PlayerHealthController playerHealth = HitInfo.GetComponent<PlayerHealthController>();
            playerHealth.TakingDame(AttackDame, transform.position);
        }
    }
    public void EnemyAttackSquare(Vector2 AttackRange, Transform AttackHitBox, int AttackDame)
    {
        Collider2D HitInfo = Physics2D.OverlapBox(AttackHitBox.transform.position, AttackRange, 0f, Player);
        if (HitInfo != null)
        {
            PlayerHealthController playerHealth = HitInfo.GetComponent<PlayerHealthController>();
            playerHealth.TakingDame(AttackDame, transform.position);
        }

    }

    public void PlayerAttack(float attackRange, Transform attackHitBox, int attackDamage)
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackHitBox.position, attackRange, Enemy);
        var hitItems = Physics2D.OverlapCircleAll(attackHitBox.position, attackRange, ContainItem);

        if (hitItems.Length > 0)
        {
            foreach (var hit in hitItems)
            {
                var anim = hit.GetComponent<Animator>();
                anim?.Play("destroy");
            }
        }
        if (hitEnemies.Length > 0)
        {
            AudioManager.Instance.PlaySFX("swordhit");
            foreach (var hit in hitEnemies)
            {
                var enemyHealth = hit.GetComponent<EnemyHeath>();
                enemyHealth?.takingDame(attackDamage, transform.position);
            }
        }
        else
        {
            AudioManager.Instance.PlaySFX("swordmiss" + Random.Range(1, 4).ToString());
        }
    }

    public void PlayerThowingSwordAttack(float AttackRange, Transform AttackHitBox, int AttackDame)
    {
        var hitEnemies = Physics2D.OverlapCircle(AttackHitBox.position, AttackRange, Enemy);
        var hitItems = Physics2D.OverlapCircle(AttackHitBox.position, AttackRange, ContainItem);
        if (hitItems != null)
        {
            var anim = hitItems.GetComponent<Animator>();
            anim?.Play("destroy");
        }
        if (hitEnemies != null)
        {
            AudioManager.Instance.PlaySFX("swordhit");
            var enemyHealth = hitEnemies.GetComponent<EnemyHeath>();
            enemyHealth?.takingDame(AttackDame, transform.position);

        }
        else
        {
            AudioManager.Instance.PlaySFX("swordmiss" + Random.Range(1, 4).ToString());
        }
    }





}
