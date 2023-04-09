using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VendorShopItem : MonoBehaviour
{
    public Item item;
    public Image icon;

    public void BuyItem()
    {
        if (PlayerController.instance.GiveMoney(item.price))
        {
            Inventory.instance.AddItem(item);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
