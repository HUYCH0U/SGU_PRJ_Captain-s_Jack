using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float Duration;
    public Transform firePos;
    private Animator anim;
    private bool isActive;
    private DealingDamage dame;


    void Start()
    {
        anim = GetComponent<Animator>();
        dame=GetComponent<DealingDamage>();

    }

    void Update()
    {
        if (!isActive)
            return;
        else
            dame.EnemyAttackCircle(0.4f, firePos, 1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isActive)
            {
                anim.Play("hit");
                Invoke("Active", 1f);
            }

        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("UnActive", Duration);
        }
    }

    private void Active()
    {
        anim.Play("on");
        isActive = true;
    }
    private void UnActive()
    {
        anim.Play("off");
        isActive = false;
    }

}
