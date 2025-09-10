using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBaseState : MonoBehaviour
{
    public int MaxHealth;
    public int SlashAttackDame;

    public static string SlashAttackDameKey = "SlashAttackDame";
    public static string MaxHealthKey = "MaxHealth";
    public static string CoinKey = "Coin";
    public void UpdateState()
    {
        PlayerPrefs.SetInt(SlashAttackDameKey, SlashAttackDame);
        PlayerPrefs.SetInt(MaxHealthKey, MaxHealth);
        PlayerPrefs.Save();
    }


    void Awake()
    {
        if(!checkState())
        SetStartState();
        else
        GetPrefabState();

    }
    public bool checkState()
    {
        return PlayerPrefs.HasKey(MaxHealthKey) &&PlayerPrefs.HasKey(SlashAttackDameKey) ;
    }
    private void SetStartState()
    {
        MaxHealth = 3;
        SlashAttackDame = 1;
        UpdateState();
    }
    public void UpdateCoin(int Coin)
    {
        PlayerPrefs.SetInt(CoinKey, Coin);
        PlayerPrefs.Save();
    }


    private void GetPrefabState()
    {
        MaxHealth=PlayerPrefs.GetInt(MaxHealthKey);
        SlashAttackDame=PlayerPrefs.GetInt(SlashAttackDameKey);
    }

    public void ClearAllState()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }


    public int getSlashAttackDame()
    {
        return SlashAttackDame;
    }
    public void setSlashAttackDame(int slashAttackDame)
    {
        SlashAttackDame = slashAttackDame;
    }
    public int getMaxHealth()
    {
        return MaxHealth;
    }

    public void setMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public int getCoin()
    {
        return PlayerPrefs.GetInt(CoinKey);
    }
  


}
