using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public bool Active;
    private Animator Helm;
    private Animator Sail;
    private GameObject Ship;
    private Rigidbody2D rb;
    private float speed;
    void Start()
    {
        Ship = transform.parent?.gameObject;
        Active = false;
        speed=0;
        Helm = GetComponent<Animator>();
        Sail = Ship.transform.Find("Sail").GetComponent<Animator>();
        rb = Ship.GetComponent<Rigidbody2D>();
    }

    private void Moving(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Update()
    {
        if (Active)
        {
            Moving(speed);
            if(speed<=7)
            speed+=Time.deltaTime;
        }
    }

    private IEnumerator ShipStart()
    {
        Helm.Play("helmon");
        Sail.Play("sailon");
        yield return new WaitForSeconds(1f);
        Sail.Play("idleon");
        Helm.Play("helmidle");
        Invoke("ChangeSceen",11f);
        Active = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Active)
            {
                if(rb.bodyType == RigidbodyType2D.Static)
                rb.bodyType = RigidbodyType2D.Dynamic;
                other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                other.gameObject.GetComponent<PlayerMovingLeftRight>().canMove = false;
                PlayerUI.instance.disable();
                StartCoroutine(ShipStart());
            }
        }
    }

    private void ChangeSceen()
    {
        Destroy(Ship);
        Operator.Instance.ChangeSceenEvent();
    }
}
