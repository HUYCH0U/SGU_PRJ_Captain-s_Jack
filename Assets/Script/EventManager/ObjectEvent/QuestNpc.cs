using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpc : MonoBehaviour
{
    private GameObject Dialouge;
    private GameObject InspectText;
    private DropingObject drop;
    void Start()
    {
        Dialouge = transform.Find("Dialouge").gameObject;
        InspectText = transform.Find("InspectText").gameObject;
        Dialouge.SetActive(false);
        InspectText.SetActive(false);
        drop=GetComponent<DropingObject>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Dialouge.SetActive(true);
            if (other.GetComponent<InventoryController>().FindObjectInInventory("Map"))
            {
                other.GetComponent<PlayerMovingLeftRight>().isColapseWithQuestNpc = true;
                InspectText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Dialouge.SetActive(false);
            InspectText.SetActive(false);
            if (other.GetComponent<InventoryController>().FindObjectInInventory("Map"))
            {
                other.GetComponent<PlayerMovingLeftRight>().isColapseWithQuestNpc = false;
            }
        }
    }
}
