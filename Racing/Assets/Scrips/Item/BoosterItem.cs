using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoosterItem : MonoBehaviour
{
    public Player Player;
    public GameObject Player_gb;

    public int BoosterItemNumber;

    void Start()
    {
        Player_gb = GameObject.Find("Player");
        Player = Player_gb.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    public void HitPlayer()
    {
        if (GameInstance.instance.CurrentPlayerItemInventory < 2)
        {
            if (GameInstance.instance.CurrentPlayerItemInventory == 0)
            {
                Player.ItemInvevtorys[0] = BoosterItemNumber;
            }
            else if (GameInstance.instance.CurrentPlayerItemInventory == 1)
            {
                Player.ItemInvevtorys[1] = BoosterItemNumber;
            }

            GameInstance.instance.CurrentPlayerItemInventory++;

            Destroy(gameObject);
        }
    }
}
