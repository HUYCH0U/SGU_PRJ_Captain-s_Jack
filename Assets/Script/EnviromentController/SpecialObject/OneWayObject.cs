using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObject : MonoBehaviour
{
    [SerializeField] private float diff;
    [SerializeField] private bool isEnterBossArea; 
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&other.transform.position.y>transform.position.y+diff)
        {

            foreach (Transform child in transform)
            {
                if(isEnterBossArea)
                PlayerUI.instance.disableCoinUI();
                child.GetComponent<Renderer>().enabled = true;
            }
            this.GetComponent<Collider2D>().isTrigger =false;
        }
    }

}

