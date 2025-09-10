using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject BreakObject;

    public void Break(bool destroy)
    {
        if(destroy)
        Destroy(gameObject);
        GameObject broke = Instantiate(BreakObject, transform.position, Quaternion.identity);
        foreach (Transform child in broke.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity =new Vector2(Random.Range(-7,7),Random.Range(5,10));
        }
        Destroy(broke, 2f);
    }

    
    public void BreakContainItem()
    {
        Destroy(gameObject);
        GameObject broke = Instantiate(BreakObject, transform.position, Quaternion.identity);
        foreach (Transform child in broke.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity =new Vector2(Random.Range(-7,7),Random.Range(5,10));
        }
        Destroy(broke, 2f);
    }


}
