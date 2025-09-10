using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainItem : MonoBehaviour
{
    [SerializeField] private GameObject[] ContainObject;
    [SerializeField] private float[] proportion;
    public bool PercentDontDrop = true;

    public void SpawnItem()
    {
        if (Random.Range(0, 2) == 0 && PercentDontDrop)
            return;
            
            float randomWeight = Random.Range(0, proportion[proportion.Length - 1]) + 1;
            for (int i = 0; i < proportion.Length; i++)
            {
                if (proportion[i] > randomWeight)
                {
                    GameObject item = Instantiate(ContainObject[i], transform.position, Quaternion.identity);
                    item.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, 7), Random.Range(5, 10));
                    return;
                }
            }
        


    }

}
