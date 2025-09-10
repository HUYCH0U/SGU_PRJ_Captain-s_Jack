using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Health
{
    public static PlayerHealthController instance;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] public Image[] hearts;
    [SerializeField] public Image heartBackGround;
    private PlayerBaseState state;
    private bool canTakeDame;
    public bool isPlayerDeath;
    public bool isTakingdame;
    private Rigidbody2D rb;
    private Flip flip;
    private PlayerAttack attack;
    private bool healingEvent;
    private float healingAmount;
    private PlayerMovingLeftRight move;
    public bool FallDeath;
    private GameObject[] EnemySet;

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
        if (instance == null)
            instance = this;
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<PlayerAttack>();
        state = GetComponent<PlayerBaseState>();
        move = GetComponent<PlayerMovingLeftRight>();
        EnemySet = GameObject.FindGameObjectsWithTag("Enemy");
        flip = GetComponent<Flip>();
        isPlayerDeath = false;
        canTakeDame = true;
        isTakingdame = false;
        healingEvent = false;
        FallDeath = false;
        healingAmount = 0;
        createHeathBar();


    }



    private IEnumerator deathEvent()
    {
        isPlayerDeath = true;
        state.UpdateCoin((int)(state.getCoin()/2));
        if (!FallDeath)
        {
            if (flip.isFacingRight)
                rb.velocity = new Vector3(-5, 15, 0);
            else
                rb.velocity = new Vector3(5, 15, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void IncreaseHealthPoint(int amount)
    {
        if (maxHealth != 7)
        {
            state.setMaxHealth(maxHealth + amount);
            maxHealth = state.getMaxHealth();
            createHeathBar();
            HealFullHp();
        }
        else
            return;
    }

    public void HealFullHp()
    {
        currentHealth = maxHealth;
        for (int i = 0; i < 7; i++)
        {
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
                hearts[i].GetComponent<Animator>().Play("idle");
            }
        }
        heartsRender();

    }

    public void createHeathBar()
    {
        maxHealth = state.getMaxHealth();
        currentHealth = maxHealth;
        for (int i = 0; i < 7; i++)
        {
            if (i < maxHealth)
            {
                Image heartBG = Instantiate(heartBackGround, hearts[i].transform.parent);
                heartBG.transform.localPosition = hearts[i].transform.localPosition;
                heartBG.transform.SetAsFirstSibling();
            }
            hearts[i].enabled = false;
        }
        heartsRender();
    }

    public void TakingDame(int amount, Vector3 enemypos)
    {
        if (canTakeDame)
        {
            AudioManager.Instance.PlaySFX("takingdame");
            canTakeDame = false;
            if (amount >= currentHealth && !isPlayerDeath)
            {
                attack.SwordOnPlayerDeath();
                StartCoroutine(deathEvent());
                currentHealth = 0;
            }
            else
            {
                currentHealth -= amount;
                if (!isTakingdame)
                    StartCoroutine(knockBack(enemypos));
                IgnoreColliderEnemy();
                isTakingdame = true;
                Invoke("resetcanTakedame", 1f);
            }
            heartsRender();
        }
    }

    public IEnumerator knockBack(Vector3 enemyPos)
    {
        Vector3 impactVec = Vector3.zero;
        if (enemyPos.x < transform.position.x)
            impactVec = new Vector3(5, 10, 0);
        else
            impactVec = new Vector3(-5, 10, 0);
        rb.velocity = impactVec;
        yield return new WaitForSeconds(0.5f);
        ColapseColliderEnemy();
        isTakingdame = false;

    }


    public void heartsRender()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                if (hearts[i].enabled == true)
                    StartCoroutine(hearthDisapear(hearts[i]));
            }
            if (healingEvent)
            {
                if (healingAmount == 0)
                    healingEvent = false;
                if (i == currentHealth - healingAmount)
                {
                    StartCoroutine(hearthApear(hearts[i]));
                    healingAmount--;
                }
            }
        }   
    }

    public void Healing(int amount)
    {
        if (currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
            healingEvent = true;
            healingAmount = amount;
            heartsRender();
        }
        else
        {
            healingAmount = maxHealth - currentHealth;
            currentHealth = maxHealth;
            healingEvent = true;
            heartsRender();

        }
    }

    private IEnumerator hearthDisapear(Image hearth)
    {
        hearth.GetComponent<Animator>().Play("HearthDisapear");
        yield return new WaitForSeconds(0.5f);
        hearth.enabled = false;
    }
    private IEnumerator hearthApear(Image hearth)
    {
        hearth.GetComponent<Animator>().Play("HearthApear");
        yield return new WaitForSeconds(0.5f);
        hearth.GetComponent<Animator>().Play("idle");
    }


    private void resetcanTakedame()
    {
        canTakeDame = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHeath heath = other.gameObject.GetComponent<EnemyHeath>();
            if (!heath.isDeath && heath.isDealDameOnContact)
                TakingDame(1, other.transform.position);
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            TakingDame(1, other.transform.position);
        }
        if (other.gameObject.CompareTag("DeathZone"))
        {
            if (!isPlayerDeath)
            {
                FallDeath = true;
                TakingDame(maxHealth, other.transform.position);
                move.FreezePosition();
            }
        }

    }


    public void IgnoreColliderEnemy()
    {
        foreach (GameObject enemy in EnemySet)
        {
            if (enemy != null)
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), true);
        }
    }

    public void ColapseColliderEnemy()
    {
        foreach (GameObject enemy in EnemySet)
        {
            if (enemy != null)
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), false);
        }
    }
}
