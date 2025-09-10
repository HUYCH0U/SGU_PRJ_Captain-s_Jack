using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEvilCaptain : MonoBehaviour
{
    [SerializeField] private float ActiveRange;
    [SerializeField] private float AttackRange;
    [SerializeField] private float speed;
    private float chaseSpeed;
    private float moveSpeed;
    public GameObject EndGameChest;
    private BaseAnimation anim;
    private PlatformCheck check;
    private Rigidbody2D rb;
    private Flip flip;
    public bool isRuning;
    private CheckPlayer checkplayer;
    private GameObject player;
    private EnemyHeath health;
    private DealingDamage dame;
    public Transform attackPos;
    private bool Awake;
    private float changeSideCount;
    private int RandomMoveHorizontal;
    private State CurrentState;
    private float AttackCountDown;
    private string PrefabName = "CaptainBomb";
    private float MinRange;
    private float MaxRange;
    private float Height;
    private float SkillCoutDown;
    private float TurnCoutDown;
    private int BombTurn;
    private bool canCheck;
    private bool isBombCoutDown;
    private float BombCoutDown;
    private enum State
    {
        Skill,
        Attack,
        RandomMove,

    }

    void Start()
    {
        isRuning = true;
        Awake = false;
        canCheck = true;
        player = GameObject.FindGameObjectWithTag("Player");
        checkplayer = GetComponent<CheckPlayer>();
        dame = GetComponent<DealingDamage>();
        health = GetComponent<EnemyHeath>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
        anim = GetComponent<BaseAnimation>();
        changeSideCount = Random.Range(1, 3);
        AttackCountDown = Random.Range(4, 6);
        BombTurn = Random.Range(3, 6);
        RandomMoveHorizontal = -1;
        chaseSpeed = 6.5f;
        moveSpeed = 5;
        MinRange = 45;
        MaxRange = 74;
        Height = 65;
        SkillCoutDown = 10;
        TurnCoutDown = 3;
        BombCoutDown = 0.5f;
    }

    public void Attack()
    {
        dame.EnemyAttackCircle(1, attackPos, 2);
    }


    private void FoundPlayer()
    {
        anim.isFoundPlayer = true;
        anim.StartModifyAnim(1.5f, "found");
        Invoke("resetFoundPlayer", 1.5f);
        CurrentState = State.RandomMove;
        anim.StartFoundPlayerDialogue();
        isBombCoutDown = false;
        Awake = true;
    }

    void Update()
    {
        if (anim.isFoundPlayer)
            return;
        if (health.isTakingDame && !anim.isAttack || health.isDeath)
        {
            if (health.CurrentHealth <= player.GetComponent<PlayerBaseState>().getSlashAttackDame() + 2)
                health.CanKnockBack = true;
            if (!Awake)
            {
                FoundPlayer();
                return;
            }
            anim.isAttack = false;
            if (health.isDeath)
            {
                Invoke("EndGameEvent", 3f);
            }
            anim.HealthAnimation();
            return;
        }
        if (player.GetComponent<PlayerHealthController>().isPlayerDeath)
        {
            anim.PlayAnim("idle");
            return;
        }

        if (checkplayer.CheckPlayerInFront(ActiveRange) && !Awake)
        {
            FoundPlayer();
        }
        if (anim.isAttack)
            return;
        if (Awake)
        {
            if (SkillCoutDown > 0)
                SkillCoutDown -= Time.deltaTime;
            else if (SkillCoutDown <= 0 && CurrentState != State.Skill)
            {
                isBombCoutDown = !isBombCoutDown;
                health.canTakeDame = false;
                CurrentState = State.Skill;
            }
            if (AttackCountDown > 0)
                AttackCountDown -= Time.deltaTime;
            if (AttackCountDown <= 0 && SkillCoutDown > 0)
            {
                AttackCountDown = Random.Range(4, 6);
                CurrentState = State.Attack;
                anim.PlayModifiDialouge(Dialogue.Attack);
            }

            if (CurrentState == State.RandomMove)
            {
                anim.baseAnimation(isRuning);
                RandomMoveState();
            }
            else if (CurrentState == State.Attack)
            {
                AttackState();
            }
            else
            {
                SkillState();
            }
        }
    }

    private void RandomMoveState()
    {
        if (speed != moveSpeed)
            speed = moveSpeed;
        if (changeSideCount > 0)
            changeSideCount -= Time.deltaTime;
        if (changeSideCount <= 0 || check.CheckLeftRight(1f) && canCheck
        || Vector2.Distance(player.transform.position, this.transform.position) <= AttackRange && canCheck)
        {
            canCheck = false;
            Invoke("resetcanCheck", 0.5f);
            changeSideCount = Random.Range(1, 3);
            RandomMoveHorizontal = -RandomMoveHorizontal;
        }

        if (RandomMoveHorizontal == 1)
        {
            flip.LookAtPlayer();
            rb.velocity = new Vector2(checkplayer.GetPlayerSide(player) * speed, rb.velocity.y);
        }
        else
        {
            flip.NotLookAtPlayer();
            rb.velocity = new Vector2(-checkplayer.GetPlayerSide(player) * speed, rb.velocity.y);
        }
    }

    private void AttackState()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) <= AttackRange ||
        checkplayer.heightDiff(player) && Vector2.Distance(player.transform.position, this.transform.position) < AttackRange)
        {
            rb.velocity = Vector2.zero;
            anim.StartBaseStaticAttack(0.75f);
            CurrentState = State.RandomMove;
        }
        else if (!anim.isAttack)
        {
            if (speed != chaseSpeed)
                speed = chaseSpeed;
            flip.NotLookAtPlayer();
            anim.baseAnimation(isRuning);
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newposition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newposition);
        }
    }

    private void SkillState()
    {
        anim.PlayAnim("skill");
        RandomMoveState();
        player.GetComponent<PlayerHealthController>().IgnoreColliderEnemy();
        if (TurnCoutDown > 0)
            TurnCoutDown -= Time.deltaTime;
        else
        {
            TurnCoutDown = 2;
            anim.PlayModifiDialouge(Dialogue.Bomb);
            if (isBombCoutDown)
                StartCoroutine(SpawnBombOneByOne());
            else
                SpawnBomb();
            BombTurn--;
            if (BombTurn < 0)
            {
                AttackCountDown = Random.Range(3, 5);
                CurrentState = State.RandomMove;
                SkillCoutDown = Random.Range(12, 17);
                BombTurn = Random.Range(3, 6);
                health.canTakeDame = true;
                player.GetComponent<PlayerHealthController>().ColapseColliderEnemy();
            }
        }
    }

    private void SpawnBomb()
    {
        for (float i = MinRange; i <= MaxRange; i += 5)
        {
            Vector2 SpawnPosition = new Vector2(i, Height);
            StaticEnemyObjectPooling.instance.SpawningObjectWithPosition(SpawnPosition, PrefabName);
        }
        if (MinRange == 45)
            MinRange = 50;
        else
            MinRange = 45;
    }

    private IEnumerator SpawnBombOneByOne()
    {
        for (float i = MinRange; i <= MaxRange; i += 5)
        {
            Vector2 SpawnPosition = new Vector2(i, Height);
            StaticEnemyObjectPooling.instance.SpawningObjectWithPosition(SpawnPosition, PrefabName);
            yield return new WaitForSeconds(BombCoutDown);
        }

    }
    private void resetFoundPlayer()
    {
        anim.isFoundPlayer = false;
    }
    private void resetcanCheck()
    {
        canCheck = true;
    }
    private void EndGameEvent()
    {
        EndGameChest.GetComponent<Rigidbody2D>().gravityScale = 4;
    }
}
