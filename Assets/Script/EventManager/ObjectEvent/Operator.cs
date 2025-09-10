using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Operator : MonoBehaviour
{
    public static Operator Instance;
    private GameObject player;
    private PlayerPosController playerPos;
    private PlayerAnimationController playerAnim;
    private PlayableDirector timeline;
    public bool isPlayAdvise;

    public void getPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.GetComponent<PlayerPosController>();
        playerAnim = player.GetComponent<PlayerAnimationController>();
    }

    public void GetTimeLine()
    {
        timeline = GameObject.FindGameObjectWithTag("TimeLine").GetComponent<PlayableDirector>();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        isPlayAdvise = false;
        PlayTimeLine();
    }

    public void PlayTimeLine()
    {
        GetTimeLine();
        getPlayer();
        if (timeline != null && !timeline.GetComponent<TimeLineController>().isPlayed)
        {
            timeline.GetComponent<TimeLineController>().isPlayed = true;
            playerPos.deleteCheckPoint();
            player.GetComponent<PlayerBaseState>().ClearAllState();
            timeline.Play();
        }
        else
            return;
        AudioManager.Instance.PlayMusic();
    }

    public void DeathEvent()
    {
        StartCoroutine(RespawnAnim());
    }

    public void ChangeSceenEvent()
    {
        StartCoroutine(ChangeScreenAnim());
    }
    public void EndGameEvent()
    {
        StartCoroutine(EndGameAnim());
    }

    private IEnumerator RespawnAnim()
    {
        ChangeScreenEffect.Instance.gameObject.SetActive(true);
        ChangeScreenEffect.Instance.PrepareRespawnScreen();
        ChangeScreenEffect.Instance.Coverscreen = true;
        getPlayer();
        yield return new WaitForSeconds(2);
        playerPos.reloadScene();
        yield return new WaitForSeconds(0.001f);
        getPlayer();
        if (!playerAnim.sword.IsHaveSword)
            playerAnim.sword.IsHaveSword = true;
        playerAnim.move.canMove = false;
        ChangeScreenEffect.Instance.Uncoverscreen = true;
        playerAnim.PlayAnim("invisible");
        playerAnim.isPlayerRespawn = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(playerAnim.PlayAppearAnim());

    }
    private IEnumerator ChangeScreenAnim()
    {
        ChangeScreenEffect.Instance.gameObject.SetActive(true);
        ChangeScreenEffect.Instance.PrepareChangeScreen();
        ChangeScreenEffect.Instance.Coverscreen = true;
        getPlayer();
        playerPos.deleteCheckPoint();
        yield return new WaitForSeconds(2);
        AudioManager.Instance.StopMusic();
        yield return new WaitForSeconds(2);
        playerPos.loadnextScene();
        AudioManager.Instance.chap++;
        isPlayAdvise = false;
        AudioManager.Instance.PlayMusic();
        yield return new WaitForSeconds(0.2f);
        ChangeScreenEffect.Instance.Uncoverscreen = true;
    }
    private IEnumerator EndGameAnim()
    {
        ChangeScreenEffect.Instance.gameObject.SetActive(true);
        ChangeScreenEffect.Instance.PrepareEndGameScreen();
        ChangeScreenEffect.Instance.Coverscreen = true;
        getPlayer();
        playerPos.deleteCheckPoint();
        player.GetComponent<PlayerBaseState>().ClearAllState();
        yield return new WaitForSeconds(2);
        AudioManager.Instance.StopMusic();
        yield return new WaitForSeconds(2);
        playerPos.LoadtostartScene();
        yield return new WaitForSeconds(0.2f);
        AudioManager.Instance.chap = 1;
        AudioManager.Instance.StopMusic();
        ChangeScreenEffect.Instance.Uncoverscreen = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(transform.parent.gameObject);
    }

    public void Exit()
    {
        playerPos.deleteCheckPoint();
        getPlayer();
        AudioManager.Instance.chap = 1;
        AudioManager.Instance.StopMusic();
        playerPos.LoadtostartScene();
        Destroy(transform.parent.gameObject);
    }



}
