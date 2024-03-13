using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ShopCanvas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitInstance()
    {
        if (GameInstance.instance == null)
        {
            return;
        }
        GameInstance.instance.CurrentPlayerEngineLever = 0;
        GameInstance.instance.MaxPlayerEngineLever = 0;
        GameInstance.instance.CurrentStageLevel = 1;
        GameInstance.instance.MaxPlayerItemInventory = 0;
        GameInstance.instance.CurrentStageLevel = 1;
    }

    public void GameStart()
    {
        InitInstance();
    }

    public void ShopEnter()
    {
        ShopCanvas.SetActive(true);

        if (GameInstance.instance.bGamePlaying)
        {       
            Time.timeScale = 0;
        }
    }
}
