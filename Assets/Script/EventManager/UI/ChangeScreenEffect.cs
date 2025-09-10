using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenEffect : MonoBehaviour
{
    public static ChangeScreenEffect Instance;
    public TextMeshProUGUI text;
    public static string RespawnText = "Ban da bi danh guc";
    public static string ChangeSceenText = "Dang di chuyen...";
    public static string EndGameText = "Ket thuc";
    public static string EmptyText = "";
    public Vector2 OriginalPos;
    public bool Coverscreen;
    public bool Uncoverscreen;
    public Animator anim;
    private string[] ChangeSceenAnimtionList = { "Gray", "Brown", "Pink", "Yellow", "Blue" };


    void Start()
    {
        if (Instance == null)
            Instance = this;
        OriginalPos = transform.position;
        Coverscreen = false;
        Uncoverscreen = false;
        text = GetComponentInChildren<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }

    public void PrepareRespawnScreen()
    {
        text.text=RespawnText;
        anim.Play(ChangeSceenAnimtionList[Random.Range(0, ChangeSceenAnimtionList.Length)]);
    }
    public void PrepareChangeScreen()
    {
        text.text=ChangeSceenText;
        anim.Play(ChangeSceenAnimtionList[Random.Range(0, ChangeSceenAnimtionList.Length)]);
    }
    public void PrepareEndGameScreen()
    {
        text.text=EndGameText;
        anim.Play("Brown");
    }


    void Update()
    {
        if (Coverscreen)
            CoverScreen();
        else if (Uncoverscreen)
            UncoverScreen();
    }

    public void CoverScreen()
    {
        if (transform.position.x >= 960)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * 2000, transform.position.y);
        }
        else
        {
            transform.position=new Vector2(960,transform.position.y);
            Coverscreen = false;
        }
    }

    public void UncoverScreen()
    {
        if (transform.position.x >= -960)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * 2000, transform.position.y);
        }
        else
        {
            transform.position=new Vector2(-960,transform.position.y);
            Uncoverscreen = false;
            gameObject.SetActive(false);
            if (transform.position.x != OriginalPos.x)
                transform.position = OriginalPos;
        }
    }





}
