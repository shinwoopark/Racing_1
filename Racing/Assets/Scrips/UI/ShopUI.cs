using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
        if (GameInstance.instance.bGamePlaying)
        {
            Time.timeScale= 1;
        }
    }
}
