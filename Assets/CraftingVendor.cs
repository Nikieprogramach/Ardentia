using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingVendor : MonoBehaviour
{
    public GameObject Player;

    public GameObject vendorShop;

    public GameObject[] recepieSlots;
    public CraftingRecepie[] itemsToCraft;

    public bool shopIsOpen = false;
    public float shopRange;

    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.GetComponent<Transform>().position) > shopRange && shopIsOpen == true)
        {
            OnClickOpenShop();
        }
    }

    public void SetItems()
    {
        for (int i = 0; i < itemsToCraft.Length; i++)
        {
            recepieSlots[i].SetActive(true);
            recepieSlots[i].GetComponent<CraftingVendorRecepieSlot>().SetSlot(itemsToCraft[i], Player);
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
            vendorShop.SetActive(true);
            shopIsOpen = true;
            SetItems();
        }
    }
}
