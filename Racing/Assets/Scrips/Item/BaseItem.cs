using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public BoosterItem BoosterItem;

    public MoneyItem MoneyItem;

    public ShopItem ShopItem;

    public bool bBoosterItem;

    public bool bMoneyItem;

    void Start()
    {
        
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(0, 30, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (bBoosterItem)
                BoosterItem.HitPlayer();
            else if(bMoneyItem)
                MoneyItem.HitPlayer();
            else
                ShopItem.HitPlayer();
        }      
    }
}
