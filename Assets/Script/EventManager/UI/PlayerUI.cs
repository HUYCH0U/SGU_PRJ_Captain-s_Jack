using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    private GameObject CoinUI;
    void Start()
    {
        if(instance == null)
        instance=this;
    }

    public void disableCoinUI()
    {
        CoinUI = transform.Find("CoinUI").gameObject;
        CoinUI.SetActive(false);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }
    public void eneble()
    {
        gameObject.SetActive(true);
    }
}
