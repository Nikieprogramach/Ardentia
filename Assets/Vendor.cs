using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public GameObject vendorShop;

    public GameObject[] slots;
    public Item[] itemsToSell;

    public bool shopIsOpen = false;

    public void SetItems()
    {
        for(int i = 0; i < itemsToSell.Length; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].GetComponent<VendorShopItem>().item = itemsToSell[i];
            slots[i].GetComponent<VendorShopItem>().icon.gameObject.SetActive(true);
            slots[i].GetComponent<VendorShopItem>().icon.sprite = itemsToSell[i].icon;
        }
    }

    public void OnClickOpenShop()
    {
        if (shopIsOpen)
        {
            vendorShop.SetActive(false);
            shopIsOpen = false;
        }
        else
        {
            SetItems();
            vendorShop.SetActive(true);
            shopIsOpen = true;
        }
    }
}
