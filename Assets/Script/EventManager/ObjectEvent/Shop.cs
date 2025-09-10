using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject InspectText;
    void Start()
    {
        InspectText = transform.Find("InspectText").gameObject;
        InspectText.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InspectText.SetActive(true);
            other.GetComponent<PlayerMovingLeftRight>().isColapseWithShop = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InspectText.SetActive(false);
            other.GetComponent<PlayerMovingLeftRight>().isColapseWithShop = false;
        }
    }
}

