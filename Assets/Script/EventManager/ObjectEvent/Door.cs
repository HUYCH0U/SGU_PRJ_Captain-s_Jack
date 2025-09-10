using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door instance;
    private Animator anim;
    private GameObject InspectText;
    private GameObject KeyRequire;
    void Start()
    {
        if (instance == null)
            instance = this;
        anim=GetComponent<Animator>();
        InspectText = transform.Find("InspectText").gameObject;
        KeyRequire = transform.Find("KeyRequire").gameObject;
        InspectText.SetActive(false);
        KeyRequire.SetActive(false);

    }

    public void Open()
    {
        StartCoroutine(open());
    }

    private IEnumerator open()
    {
        anim.Play("open");
        yield return new WaitForSeconds(1);
        anim.Play("openidle");
        this.GetComponent<Collider2D>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyRequire.SetActive(true);
            if (other.gameObject.GetComponent<InventoryController>().FindObjectInInventory("Key"))
            {
                other.gameObject.GetComponent<PlayerMovingLeftRight>().isColapseWithDoor = true;
                InspectText.SetActive(true);
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyRequire.SetActive(false);
            InspectText.SetActive(false);
            if (other.gameObject.GetComponent<InventoryController>().FindObjectInInventory("Key"))
            {
                other.gameObject.GetComponent<PlayerMovingLeftRight>().isColapseWithDoor = false;
            }
        }
    }

}
