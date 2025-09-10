using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator anim;
    public GameObject[] itemList;
    [SerializeField] private float spawnInterval = 0.5f;
    public int[] itemQuantity;
    private float lastSpawnTime;
    private int currentItemIndex = 0;
    private int currentItemCount = 0;
    private bool isOpen = false;
    private bool isDone = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isOpen)
            return;
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            if (currentItemIndex < itemList.Length)
            {
                GameObject item = Instantiate(itemList[currentItemIndex], transform.position, Quaternion.identity);
                item.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, 7), Random.Range(10, 15));
                lastSpawnTime = Time.time;
                currentItemCount++;
                if (currentItemCount >= itemQuantity[currentItemIndex])
                {
                    currentItemIndex++;
                    currentItemCount = 0;
                     if (currentItemIndex >= itemList.Length)
                    {
                        anim.Play("close"); 
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!isDone)
        {
            anim.Play("open");
        }
    }

    public void open()
    {
        anim.Play("idleopen");
        isOpen = true;
    }
    public void close()
    {
        anim.Play("idle");
        isOpen = false;
        isDone=true;
    }
}
