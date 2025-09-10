using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingObject : MonoBehaviour
{
    public void Drop()
    {
        this.GetComponent<Rigidbody2D>().GetComponent<Collider2D>().enabled = false;
    }
    public void DropAndDestroy(float time)
    {
        this.GetComponent<Rigidbody2D>().GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,time);
    }
}
