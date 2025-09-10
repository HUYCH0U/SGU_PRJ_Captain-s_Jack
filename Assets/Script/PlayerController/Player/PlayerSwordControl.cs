using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordControl : MonoBehaviour
{
    public bool IsHaveSword;
    [SerializeField] private GameObject SwordPrefab;
    [SerializeField] private Transform SwordPos;
    public void SpawnSword(Vector3 throwway, bool isPlayerDeath,int dame)
    {
        GameObject sword = Instantiate(SwordPrefab, SwordPos.position, SwordPos.rotation);
        Sword newsword = sword.GetComponent<Sword>();
        newsword.way = throwway.normalized;
        newsword.isPlayerDeath = isPlayerDeath;
        newsword.Damage=dame;
        if (isPlayerDeath)
        {
            newsword.GetComponent<Rigidbody2D>().gravityScale = 4;
            newsword.speed = 12f;
        }
    }

}
