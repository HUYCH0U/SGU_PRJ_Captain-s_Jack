using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingLeftRight : MonoBehaviour
{
    [SerializeField] private float speed;
    public float horizontal;
    private Rigidbody2D rb;
    private Flip flip;
    private PlayerHealthController health;
    private PlayerAttack attack;
    private InventoryController inventory;
    public bool isColapseWithShop;
    public bool isColapseWithQuestNpc;
    public bool isColapseWithDoor;
    public bool canMove;
    private PlayerInspect inspect;

    void Start()
    {
        inventory = GetComponent<InventoryController>();
        health = GetComponent<PlayerHealthController>();
        flip = GetComponent<Flip>();
        inspect=GetComponent<PlayerInspect>();
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<PlayerAttack>();
        canMove = true;
        isColapseWithShop = false;
        isColapseWithQuestNpc = false;
        if(Time.timeScale==0)
        Time.timeScale=1;
    }

    void Update()
    {
        if (health.isPlayerDeath || health.isTakingdame || !canMove)
            return;
        horizontal = Input.GetAxis("Horizontal");
        if (!attack.isAttack)
            flip.PlayerFlip();
        MovingEvent();
        inspect.EventInspect(isColapseWithShop,isColapseWithQuestNpc,isColapseWithDoor,inventory);
        inspect.InventoryInspect(inventory);
      
    }

    private void MovingEvent()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void FreezePosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }
    public void ReleasePosition()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void EnebleControl()
    {
        canMove=true;
    }
    public void DisableControl()
    {
        canMove=false;
        rb.velocity=Vector2.zero;
    }
}
