using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyObjectPooling : MonoBehaviour
{
    public static StaticEnemyObjectPooling instance;
    [SerializeField] private GameObject[] ObjectPreb;
    [SerializeField] private int[] Amount;
    [SerializeField] private int TotalAmount;
    private List<GameObject> PoolObject;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        foreach(int amount in Amount)
        TotalAmount +=amount ;
        PoolObject = new List<GameObject>();
        addObjectToPooling(ObjectPreb, Amount);

    }

    private void addObjectToPooling(GameObject[] ObjectList, int[] Amount)
    {
        int index=0;
        foreach (GameObject Object in ObjectList)
        {
            if (Object != null && Amount[index] != 0)
            {
                for (int i = 0; i < Amount[index]; i++)
                {
                    GameObject newObject = Instantiate(Object);
                    newObject.SetActive(false);
                    PoolObject.Add(newObject);
                }
            }
            index++;
        }
    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < TotalAmount; i++)
        {
            if (!PoolObject[i].activeInHierarchy && PoolObject[i].name.Equals(name))
            {
                return PoolObject[i];
            }
        }
        return null;
    }

    public void SpawningObject(Transform Pos, string name)
    {
        GameObject Object = GetPooledObject(name + "(Clone)");
        if (Object != null)
        {
            Object.transform.position = Pos.position;
            Object.SetActive(true);
        }
    }

    public void SpawningObjectWithPosition(Vector3 Pos, string name)
 {
        GameObject Object = GetPooledObject(name + "(Clone)");
        if (Object != null)
        {
            Object.transform.position = Pos;
            Object.SetActive(true);
        }
    }
}
