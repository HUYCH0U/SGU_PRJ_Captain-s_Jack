using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    private Animator anim;
    private GameObject InspectText;

    void Start()
    {
        InspectText=transform.Find("InspectText").gameObject;
        InspectText.SetActive(false);
        anim = GetComponent<Animator>();
    }

    void Update()
    {  
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPosController playerPos = FindObjectOfType<PlayerPosController>();
                PlayerHealthController playerHealth = FindObjectOfType<PlayerHealthController>();
                CheckForInput(playerPos.gameObject);
                if(playerHealth.CurrentHealth!=playerHealth.MaxHealth)
                playerHealth.HealFullHp();
                Destroy(InspectText);
                this.enabled = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if(InspectText!=null)
            InspectText.SetActive(true);

        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if(InspectText!=null)
            InspectText.SetActive(false);
        }
    }

    private void CheckForInput(GameObject player)
    {
        PlayerPosController pos = player.GetComponent<PlayerPosController>();
        StartCoroutine(PopAnim());
        pos.setCheckPoint(transform.position.x, transform.position.y);
    }

    private IEnumerator PopAnim()
    {
        anim.Play("popup");
        yield return new WaitForSeconds(1.5f);
        anim.Play("idle");
    }
}
