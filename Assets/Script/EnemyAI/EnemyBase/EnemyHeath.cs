using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyHeath : Health
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float TakingDameTime;
    [SerializeField]public bool isDealDameOnContact=true;
    public bool CanKnockBack = true;
    public bool isDeath;
    public bool isTakingDame;
    public bool canTakeDame;
    private Rigidbody2D rb;
    public override int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public override int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isTakingDame = false;
        canTakeDame = true;
        isDeath = false;
        currentHealth = maxHealth;
    }

    public void takingDame(int amount, Vector3 playerPos)
    {
        if (canTakeDame && !isTakingDame)
        {
            if (currentHealth <= amount)
            {
                currentHealth = 0;
                isDeath = true;
                canTakeDame = false;
            }
            else
            {
                isTakingDame = true;
                currentHealth -= amount;
            }
            if (CanKnockBack)
                StartCoroutine(knockBack(playerPos,TakingDameTime));
            else
                Invoke("resetTakingdame",TakingDameTime);


        }
    }


    public IEnumerator knockBack(Vector3 playerPos,float takingdametime)
    {
        Vector3 impactVec = Vector3.zero;
        if (playerPos.x < transform.position.x)
            impactVec = new Vector3(5, 12, 0);
        else
            impactVec = new Vector3(-5, 12, 0);
        rb.velocity = impactVec;
        yield return new WaitForSeconds(takingdametime);
        isTakingDame = false;
    }

    private void resetTakingdame()
    {
        isTakingDame = false;
    }

}
