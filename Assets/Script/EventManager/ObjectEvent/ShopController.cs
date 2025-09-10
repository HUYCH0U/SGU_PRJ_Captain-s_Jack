using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public bool isGoingUp;
    public bool isGoingDown;
    void Start()
    {
        isGoingUp = false;
        isGoingDown = false;

    }

    void Update()
    {
        if (isGoingUp)
            goUp();
        else if (isGoingDown)
            Dropdown();
    }
    public void Dropdown()
    {
        if (transform.position.y > 540)
            transform.position += Vector3.down * 1500 * Time.deltaTime;
        else
            isGoingDown = false;
    }

    public void goUp()
    {
        if (transform.position.y <= 1470)
            transform.position += Vector3.up * 1500 * Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
            isGoingUp = false;
        }
    }
}
