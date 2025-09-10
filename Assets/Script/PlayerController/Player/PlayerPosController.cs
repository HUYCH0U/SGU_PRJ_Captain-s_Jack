using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosController : MonoBehaviour
{
    private const string PosX = "PlayerPosX";
    private const string PosY = "PlayerPosY";
    void Start()
    {
        SpawnAtCheckPoint();
    }

    public void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void LoadtostartScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void loadnextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SpawnAtCheckPoint()
    {
        if (checkCheckPoint())
            transform.position = new Vector2(getX(), getY() - 0.78f);
        else
            return;
    }
    public void setCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat(PosX, x);
        PlayerPrefs.SetFloat(PosY, y);
    }

    public bool checkEqual(float x, float y)
    {
        return x == getX() && y == getY();
    }

    public void deleteCheckPoint()
    {
        if (checkCheckPoint())
        {
            PlayerPrefs.DeleteKey(PosX);
            PlayerPrefs.DeleteKey(PosX);
        }
    }

    private bool checkCheckPoint()
    {
        return PlayerPrefs.GetFloat(PosX) != 0f && PlayerPrefs.GetFloat(PosY) != 0f;
    }

    public float getX()
    {
        if (checkCheckPoint())
            return PlayerPrefs.GetFloat(PosX);
        else
            return 0;
    }
    public float getY()
    {
        if (checkCheckPoint())
            return PlayerPrefs.GetFloat(PosY);
        else
            return 0;
    }

}
