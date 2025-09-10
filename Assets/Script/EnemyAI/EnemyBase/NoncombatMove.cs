using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoncombatMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Flip flip;
    private PlatformCheck check;
    private bool canCheck;

    void Start()
    {
        canCheck = true;
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PlatformCheck>();
        flip = GetComponent<Flip>();
    }
    public void GoLeftAndRight(int speed)
    {
        if (check.CheckLeftRight(1f) && canCheck)
        {
            canCheck = false;
            flip.FlipOnce();
            Invoke("resetCheck", 0.25F);
        }
        rb.velocity = transform.right * speed;
    }

    private void resetCheck()
    {
        canCheck = true;
    }




}
