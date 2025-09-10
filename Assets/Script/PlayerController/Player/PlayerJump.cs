using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] public float RayLength;
    public bool isJump;
    public bool isGrounding;
    public bool IsPlayerGround;
    private PlayerAttack attack;
    private Rigidbody2D rb;
    private PlatformCheck check;
    private PlayerHealthController health;
    private PlayerMovingLeftRight move;

    void Start()
    {
        health=GetComponent<PlayerHealthController>();
        attack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        move=GetComponent<PlayerMovingLeftRight>();
        isJump = false;
        isGrounding = false;
    }

    void Update()
    {
        if (attack.isAttack||health.isPlayerDeath||!move.canMove)
            return;
        if (isGrounding && Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySFX("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounding = false;
        }
        isGrounding = check.PlayerCheckGround(RayLength);

    }

    private void resetGrounding()
    {
        isGrounding = false;
    }
}
