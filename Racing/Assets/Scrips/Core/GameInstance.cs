using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public int CurrentPlayerEngineLever;
    public int MaxPlayerEngineLever = 3;

    public int CurrentPlayerItemInventory;
    public int MaxPlayerItemInventory = 2;

    public int CurrentPlayerMoney;

    public int CurrentStageLevel;

    public bool bGamePlaying = true;

    public bool bRacing = false;

    public bool TimePause;

    private void Awake()
    {
        bGamePlaying = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
