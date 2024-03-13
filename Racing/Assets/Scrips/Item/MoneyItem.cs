using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItem : MonoBehaviour
{
    public int Money;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void HitPlayer()
    {
        GameInstance.instance.CurrentPlayerMoney += Money;

        Destroy(gameObject);
    }
}
