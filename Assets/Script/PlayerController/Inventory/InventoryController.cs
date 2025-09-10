using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject[] InventorySlot;
    public GameObject[] ObjectImg;
    public GameObject[] Object;
    public bool[] isSlotFill;
    public bool CanPickUp;
    private PlayerHealthController health;
    private PlayerMovingLeftRight move;
    private string[] QuestItem = { "Map", "Key" };


    void Awake()
    {
        health = GetComponent<PlayerHealthController>();
        move = GetComponent<PlayerMovingLeftRight>();
        isSlotFill = new bool[InventorySlot.Length];
        Object = new GameObject[InventorySlot.Length];
        for (int i = 0; i < InventorySlot.Length; i++)
        {
            isSlotFill[i] = false;
        }
        CanPickUp = true;
    }

    public void UseItem(int index)
    {
        if (isSlotFill[index])
        {
            if (Object[index].name.Contains("HealthPotion"))
            {
                if (health.CurrentHealth != health.MaxHealth)
                {
                    health.Healing(Object[index].GetComponent<HealthObject>().healingAmount);
                    deleteItem(InventorySlot[index], index);

                }
            }
            else if (Object[index].name.Contains("Map") && move.isColapseWithQuestNpc||Object[index].name.Contains("Key") && move.isColapseWithDoor)
            {
                deleteItem(InventorySlot[index], index);
            }
        }
        else
            return;
    }

    public void useItemWithName(string Name)
    {
        for (int i = 0; i < Object.Length; i++)
        {
            if (Object[i] != null && Object[i].name.Contains(Name))
            {
                UseItem(i);
                return;
            }
        }
    }

    public string getItemNameWithIndex(int index)
    {
        if(Object[index]!=null)
        return Object[index].name;
        else
        return "";
    }

    public bool FindObjectInInventory(string objName)
    {
        foreach (GameObject obj in Object)
        {
            if (obj != null && obj.name.Contains(objName))
                return true;
        }
        return false;
    }

    public void deleteItem(GameObject slot, int index)
    {
        foreach (Transform child in slot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        isSlotFill[index] = false;
        Destroy(Object[index]);
        Object[index] = null;
    }
    
    private GameObject FindImgObjectWithName(string name)
    {
        foreach (GameObject obj in ObjectImg)
        {
            if (name.Contains(obj.name))
            {
                return obj;
            }
        }
        return null;
    }

    private bool IsInventoryFull()
    {
        for (int i = 0; i < isSlotFill.Length; i++)
        {
            if (isSlotFill[i] == false)
                return false;
        }
        return true;
    }

    private void PickUpObject(GameObject obj)
    {
        for (int i = 0; i < InventorySlot.Length; i++)
        {
            if (isSlotFill[i] == false)
            {
                isSlotFill[i] = true;
                Instantiate(FindImgObjectWithName(obj.name), InventorySlot[i].transform, false);
                obj.GetComponent<Animator>().Play("disapear");
                Object[i] = Instantiate(obj);
                Object[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object") && CanPickUp && !IsInventoryFull())
        {
            Physics2D.IgnoreCollision(this.GetComponent<Rigidbody2D>().GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
            PickUpObject(other.gameObject);
            CanPickUp = false;
            Invoke("resetCanPickUp", 0.1f);
        }
    }

    private void resetCanPickUp()
    {
        CanPickUp = true;
    }

}
