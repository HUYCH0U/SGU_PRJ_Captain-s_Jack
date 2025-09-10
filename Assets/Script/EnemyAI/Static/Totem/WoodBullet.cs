using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBullet : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private int Speed;
    private Animator anim;
    private Rigidbody2D rb;
    private BreakableObject breakableObject;
    private bool ishit = false;

    void Start()
    {
        breakableObject = GetComponent<BreakableObject>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!ishit)
        {
            anim.Play("idle");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = -transform.right * Speed;
        }
        else
            rb.velocity = Vector2.zero;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ishit = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            anim.Play("explose");
            PlayerHealthController.instance.TakingDame(Damage, transform.position);
            breakableObject.Break(false);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            ishit = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            anim.Play("explose");
            breakableObject.Break(false);
        }
    }

    public void destroy()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        gameObject.SetActive(false);
        ishit = false;
    }
}
