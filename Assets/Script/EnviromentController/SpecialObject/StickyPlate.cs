using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyPlate : MonoBehaviour
{

void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        other.gameObject.transform.SetParent(gameObject.transform, true);
    }
}


void OnCollisionExit2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        other.gameObject.transform.SetParent(null);
    }
    
}
}
