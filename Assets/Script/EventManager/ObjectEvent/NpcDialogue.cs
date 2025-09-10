using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    private Dialogue dialogue;
    public int Type;
    void Start()
    {
        dialogue = transform.Find("Dialogue").GetComponent<Dialogue>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (Type)
            {
                case 1:
                    dialogue.PlayDialogueOnce(Dialogue.Hi);
                    break;
                case 2:
                    dialogue.PlayDialogueOnce(Dialogue.Hello);
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        dialogue.resetDialogue();
    }
}
