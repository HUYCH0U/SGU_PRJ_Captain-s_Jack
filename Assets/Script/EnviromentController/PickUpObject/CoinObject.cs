using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    private Animator anim;
    public int coinAmount;

    void Start()
    {
        anim =GetComponent<Animator>();
    }
    public void destroy()
    {
        anim.Play("disapear");
        Destroy(gameObject,0.1f);
    }
}
