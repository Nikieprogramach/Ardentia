using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public GameObject customCursor;

    public Item item;
    public int amountOfItems;

    public CraftingManager CraftingManager;

    public Inventory Inventory;

    public bool canRemoveItems = true;

    public GameObject itemInfoObj;

    void Start()
    {
        Init();
    }

    void Init()
    {
        itemInfoObj = ItemInfo.instance.gameObject;
    }

    public void AddItem(Item itemToAdd)
    {
        item = itemToAdd;

        icon.sprite = itemToAdd.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void DragAndDrop()
    {
        if (item == null)
        {
            return;
        }

        if (canRemoveItems)
        {
            CraftingManager.currentItem = item;
            CraftingManager.pickedUpSlot = gameObject;

            customCursor.SetActive(true);
            customCursor.GetComponent<CustomCursor>().item = item;
            Debug.Log(item);
            customCursor.GetComponent<Image>().sprite = item.icon;

            Inventory.RemoveItem(item);
            canRemoveItems = false;
        }
    }

    public void OnMouseOver()
    {
        Debug.Log("Hovering on " + gameObject.name);
        if(item != null)
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
