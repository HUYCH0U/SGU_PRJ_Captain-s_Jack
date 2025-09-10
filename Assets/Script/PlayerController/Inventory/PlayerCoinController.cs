using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerCoinController : MonoBehaviour
{
    public int CurrentCoin; 
    private PlayerBaseState state;
    public TextMeshProUGUI text;

    void Start()
    {
        state = GetComponent<PlayerBaseState>();
        if (state.getCoin() != 0)
        {
            CurrentCoin = state.getCoin();
        }else
        {
            state.UpdateCoin(0);
        }
        SetTextCoin();
    }

    public void decreaseCoin(int amount)
    {
        CurrentCoin -= amount;
        state.UpdateCoin(CurrentCoin);
        SetTextCoin();
    }
    public void CollectCoin(int amount)
    {
        CurrentCoin += amount;
        SetTextCoin();
    }

    public void SetTextCoin()
    {
        text.text = CurrentCoin.ToString();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("CoinObject"))
        {
            CoinObject Coin = other.gameObject.GetComponent<CoinObject>();
            CollectCoin(Coin.coinAmount);
            if (Coin != null)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Coin.GetComponent<Collider2D>(), true);
            }
            state.UpdateCoin(CurrentCoin);
            Coin.destroy();
        }
    }
}
