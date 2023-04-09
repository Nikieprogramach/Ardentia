using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Item item;
    public int slotIndex;
    public Image icon;

    public CraftingManager CraftingManager;

    public GameObject customCursor;

    public void DragAndDrop()
    {
        if (item == null)
        {
            return;
        }

        if(gameObject.name == "ResultSlot")
        {
            CraftingManager.currentItem = item;

            customCursor.SetActive(true);
            customCursor.GetComponent<CustomCursor>().item = item;
            customCursor.GetComponent<Image>().sprite = item.icon;

            item = null;
            icon.sprite = null;
            icon.gameObject.SetActive(false);

            foreach (GameObject slot in CraftingManager.Slots)
            {
                if(slot.GetComponent<InventorySlot>() != null)
                {
                    continue;
                }

                slot.GetComponent<CraftingSlot>().item = null;
                slot.GetComponent<CraftingSlot>().icon.sprite = null;
                slot.GetComponent<CraftingSlot>().icon.gameObject.SetActive(false);
                CraftingManager.itemList[slot.GetComponent<CraftingSlot>().slotIndex] = null;
            }
        }
        else
        {
            CraftingManager.currentItem = item;

            customCursor.SetActive(true);
            customCursor.GetComponent<CustomCursor>().item = item;
            customCursor.GetComponent<Image>().sprite = item.icon;
            item = null;
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            CraftingManager.itemList[slotIndex] = null;
        }
    }

    public void AddItemToInventory()
    {
        if(item != null)
        {
            Inventory.instance.AddItem(item);
        }
    }
}
