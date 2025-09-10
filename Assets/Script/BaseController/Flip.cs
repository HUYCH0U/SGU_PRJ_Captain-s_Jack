using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private PlayerMovingLeftRight move;
    public bool isFacingRight;
    public bool CanFlip;
    public bool EnemyisFlipped;
    private Transform player;
    void Start()
    {
        EnemyisFlipped = false;
        isFacingRight = true;
        CanFlip = true;
        move = GetComponent<PlayerMovingLeftRight>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public int getSide()
    {
        if (!EnemyisFlipped)
            return -1;
        else
            return 1;
    }
    public void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && EnemyisFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                EnemyisFlipped = false;
            }
            else if (transform.position.x < player.position.x && !EnemyisFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                EnemyisFlipped = true;
            }
        }
    }

        public void NotLookAtPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x < player.position.x && EnemyisFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                EnemyisFlipped = false;
            }
            else if (transform.position.x > player.position.x && !EnemyisFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                EnemyisFlipped = true;
            }
        }
    }



    public void FlipOnce()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void ResetFlip()
    {
        if (transform.eulerAngles.y < 1 && transform.eulerAngles.y != 0)
            FlipOnce();
    }

    public void PlayerFlip()
    {
        if(CanFlip)
        if ((isFacingRight && move.horizontal < 0f || !isFacingRight && move.horizontal > 0f) && CanFlip)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }
}
