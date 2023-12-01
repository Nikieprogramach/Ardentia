using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlots : MonoBehaviour
{
    public int slotIndex;

    public Image icon;
    public Equipment item;

    public Inventory Inventory;
    public PlayerController PlayerController;

    public CraftingManager CraftingManager;
    public GameObject customCursor;

    public GameObject itemInfoObj;

    void Start()
    {
        Init();
    }

    void Init()
    {
        itemInfoObj = ItemInfo.instance.gameObject;
    }

    public void AddToInventory()
    {
        Inventory.AddItem(item, 1);
        PlayerController.Agility -= item.agilityModifier;
        PlayerController.Strength -= item.strengthModifier;
        PlayerController.Stamina -= item.staminaModifier;
        PlayerController.Intelect -= item.intelectModifier;
        PlayerController.Spirit -= item.spiritModifier;
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        Inventory.currentEquipment[slotIndex] = null;
    }

    public void DragAndDrop()
    {
        if (item == null)
        {
            return;
        }

        CraftingManager.currentItem = item;

        customCursor.SetActive(true);
        customCursor.GetComponent<CustomCursor>().item = item;
        customCursor.GetComponent<Image>().sprite = item.icon;
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        Inventory.currentEquipment[slotIndex] = null;
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
