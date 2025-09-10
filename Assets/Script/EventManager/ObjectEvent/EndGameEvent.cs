using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameEvent : MonoBehaviour
{
    private bool isActive;

    void Start()
    {
        isActive = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")&&!isActive)
        {
        isActive=true;
        PlayerUI.instance.disable();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovingLeftRight>().canMove = false;
        Invoke("Endgame", 6);
        }
    }

    private void Endgame()
    {
        Operator.Instance.EndGameEvent();

    }
}
