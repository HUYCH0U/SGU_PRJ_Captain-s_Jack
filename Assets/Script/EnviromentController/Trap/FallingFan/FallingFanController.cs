using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingFanController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 StartPos;
    private Animator anim;
    private bool flyUp;
    void Start()
    {
        flyUp = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartPos = transform.position;
    }

    void Update()
    {
        if (flyUp)
        {
            FlyToStartPos();
        }
        else
            return;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("off");
            Invoke("DropDown", 0.5f);
        }
    }

    private void DropDown()
    {
        rb.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("Fly", 1f);
    }
    private void FlyToStartPos()
    {
        if (Vector2.Distance(StartPos, transform.position) > 0.1f)
            rb.MovePosition(Vector2.MoveTowards(rb.position, StartPos, 5 * Time.fixedDeltaTime));
        else
        {
            rb.GetComponent<Collider2D>().enabled = true;
            rb.bodyType = RigidbodyType2D.Static;
            flyUp = false;
        }
    }

    private void Fly()
    {
        anim.Play("on");
        flyUp = true;
    }
}
