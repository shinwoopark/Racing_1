using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject GameManager_gb;

    void Start()
    {
        GameManager_gb = GameObject.Find("GameManager");
        GameManager = GameManager_gb.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void HitPlayer()
    {
        GameManager.ShopEnter();

        Destroy(gameObject);
    }
}
