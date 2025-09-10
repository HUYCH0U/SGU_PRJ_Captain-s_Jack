using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSystem : MonoBehaviour
{
    public static PowerUpSystem instance;
    private TextMeshProUGUI AttackText;
    private TextMeshProUGUI HpText;
    private TextMeshProUGUI CostText;
    private Button AttackButton;
    private Button hpButton;
    private Button DoneButton;
    private GameObject player;
    private PlayerBaseState state;
    private PlayerHealthController healthController;
    private PlayerCoinController coin;
    private int Cost;
    private static string CostKey = "Cost";
    private ShopController shop;
    private Color BaseColor;

    void Start()
    {
        if (instance == null)
            instance = this;
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<ShopController>();
        player = GameObject.FindGameObjectWithTag("Player");
        state = player.GetComponent<PlayerBaseState>();
        coin = player.GetComponent<PlayerCoinController>();
        healthController = player.GetComponent<PlayerHealthController>();
        AttackText = transform.Find("AtkText").GetComponent<TextMeshProUGUI>(); ;
        HpText = transform.Find("HpText").GetComponent<TextMeshProUGUI>(); ;
        CostText = transform.Find("CostText").GetComponent<TextMeshProUGUI>(); ;
        AttackButton = transform.Find("PlusAtk").GetComponent<Button>();
        hpButton = transform.Find("PlusHp").GetComponent<Button>();
        DoneButton = transform.Find("DoneButton").GetComponent<Button>();
        hpButton.onClick.AddListener(IncreaseHealthPoint);
        AttackButton.onClick.AddListener(IncreaseAttackPoint);
        DoneButton.onClick.AddListener(Done);
        shop.gameObject.SetActive(false);
        BaseColor = CostText.color;
        if (!PlayerPrefs.HasKey(CostKey))
        {
            Cost = 50;
            PlayerPrefs.SetInt(CostKey, Cost);
        }
        else
        {
            Cost = PlayerPrefs.GetInt(CostKey);
        }

    }

    public void DropDown()
    {
        shop.gameObject.SetActive(true);
        shop.isGoingDown = true;
        drawBoard();
        player.GetComponent<PlayerMovingLeftRight>().FreezePosition();
        player.GetComponent<PlayerAttack>().canAttack = false;
    }

    public void drawBoard()
    {
        AttackText.text = state.getSlashAttackDame().ToString();
        CostText.text = Cost.ToString();

        if (coin.CurrentCoin < Cost)
            CostText.color = Color.magenta;
        else
            CostText.color = BaseColor;

        if (state.getMaxHealth() == 7)
        {
            HpText.text = "MAX";
            return;
        }
        else
            HpText.text = state.getMaxHealth().ToString();
    }
    private void IncreaseHealthPoint()
    {
        if (coin.CurrentCoin >= Cost && state.getMaxHealth() != 7)
        {
            healthController.IncreaseHealthPoint(1);
            if (state.getMaxHealth() == 7)
            {
                hpButton.gameObject.SetActive(false);
            }
            state.UpdateState();
            coin.decreaseCoin(Cost);
            increaseCost();
            drawBoard();
        }
        else
            return;
    }
    private void IncreaseAttackPoint()
    {
        if (coin.CurrentCoin >= Cost)
        {
            state.setSlashAttackDame(state.SlashAttackDame + 1);
            state.UpdateState();
            coin.decreaseCoin(Cost);
            increaseCost();
            drawBoard();
        }
        else
            return;
    }

    private void increaseCost()
    {
        Cost += 50;
        PlayerPrefs.SetInt(CostKey, Cost);
    }

    private void Done()
    {
        if (shop.isGoingUp || shop.isGoingDown)
            return;
        shop.isGoingUp = true;
        player.GetComponent<PlayerMovingLeftRight>().ReleasePosition();
        player.GetComponent<PlayerAttack>().canAttack = true;
    }


}
