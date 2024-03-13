using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMove PlayerMove;

    public int[] ItemInvevtorys;

    void Start()
    {
        
    }

    void Update()
    {
        InputItem();
    }

    void InputItem()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GameInstance.instance.CurrentPlayerItemInventory > 0)
            {
                PlayerMove.Booster(ItemInvevtorys[0]);

                ItemInvevtorys[0] = ItemInvevtorys[1];

                ItemInvevtorys[1] = 0; ;

                GameInstance.instance.CurrentPlayerItemInventory--;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Item":
                
                break;
        }
    }
}
