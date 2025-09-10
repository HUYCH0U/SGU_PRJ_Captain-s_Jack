using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspect : MonoBehaviour
{
    private GameObject Setting;
    private SettingUI settingUI;
    private PlayerMovingLeftRight move;

    void Start()
    {
        Setting = GameObject.FindGameObjectWithTag("Setting");
        settingUI = Setting.GetComponent<SettingUI>();
        Setting.SetActive(false);
        Setting.GetComponent<SettingUI>().isactive = false;
        move = GetComponent<PlayerMovingLeftRight>();
    }
    public void EventInspect(bool isColapseWithShop, bool isColapseWithQuestNpc, bool isColapseWithDoor, InventoryController inventory)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isColapseWithShop)
                PowerUpSystem.instance.DropDown();
            else if (isColapseWithQuestNpc && inventory.FindObjectInInventory("Map"))
            {
                GameObject.FindGameObjectWithTag("Drop").GetComponent<DropingObject>().Drop();
                inventory.useItemWithName("Map");

            }
            else if (isColapseWithDoor && inventory.FindObjectInInventory("Key"))
            {
                Door.instance.Open();
                inventory.useItemWithName("Key");
            }
            else
                return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingUI.isactive = !settingUI.isactive;
            if (settingUI.isactive)
            {
                Setting.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Setting.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void InventoryInspect(InventoryController inventory)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                if (!inventory.getItemNameWithIndex(i - 1).Contains("Map") || !inventory.getItemNameWithIndex(i - 1).Contains("Key"))
                    inventory.UseItem(i - 1);
            }
        }
    }
}
