using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VendorShopItem : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Text price;

    public GameObject itemInfoObj;

    void Start()
    {
        Init();
    }

    void Init()
    {
        itemInfoObj = ItemInfo.instance.gameObject;
    }

    public void BuyItem()
    {
        if (PlayerController.instance.GiveMoney(item.price))
        {
            Inventory.instance.AddItem(item, 1);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void OnMouseOver()
    {
        Debug.Log("Hovering on " + gameObject.name);
        if (item != null)
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(item);
        }

    }

    public void OnMouseExit()
    {
        Debug.Log("Exiting on " + gameObject.name);
        //itemInfoObj.GetComponent<ItemInfo>().ItemInfoShowup.SetActive(false);
        itemInfoObj.GetComponent<ItemInfo>().HideInfo();
    }
}
