using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        Canvas[] canvasObject = FindObjectsOfType<Canvas>();
        bool areMultiple = false;
        foreach(Canvas canvas in canvasObject)
        {
            if(canvas.name == "PlayerCanvas")
            {
                if(areMultiple != false)
                {
                    Destroy(gameObject);
                }
                areMultiple = true;

            }
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;

    public int space = 20;

    public Transform itemParent;

    public PlayerController PlayerController;

    InventorySlot[] slots;

    public Equipment[] currentEquipment;

    public GameObject[] equipmentSlots;

    public struct ItemSlotWithAmount
    {
        public Item item;
        public int amount;
        public bool itemInSlot;
    }

    public List<ItemSlotWithAmount> itemsTestWithAmount = new List<ItemSlotWithAmount>();

    public List<Item> items = new List<Item>();

    void Start()
    {
        Init();
    }

    void Init()
    {
        onItemChangedCallback += UpdateUI;

        slots = itemParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public bool AddItem(Item ItemToBeAdded, int amount)
    {
        if (!ItemToBeAdded.isDefaultItem)
        {
            bool sameItemExists = false;
            if(ItemToBeAdded.isStackable == true)
            {
                for (int i = 0; i < itemsTestWithAmount.Count; i++)
                {
                    if (itemsTestWithAmount[i].item == ItemToBeAdded)
                    {
                        ItemSlotWithAmount item = new ItemSlotWithAmount();
                        item.item = ItemToBeAdded;
                        item.amount = itemsTestWithAmount[i].amount + amount;
                        item.itemInSlot = true;

                        itemsTestWithAmount[i] = item;
                        sameItemExists = true;

                        if (onItemChangedCallback != null)
                        {
                            onItemChangedCallback.Invoke();
                        }

                        break;
                        
                    }
                }
            }
            if (!sameItemExists)
            {
                if (itemsTestWithAmount.Count >= space)
                {
                    return false;
                }

                ItemSlotWithAmount item = new ItemSlotWithAmount();
                item.item = ItemToBeAdded;
                item.amount = amount;

                itemsTestWithAmount.Add(item);

                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
        }
        return true;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            AddItem(oldItem, 1);

            PlayerController.Agility -= oldItem.agilityModifier;
            PlayerController.Strength -= oldItem.strengthModifier;
            PlayerController.Stamina -= oldItem.staminaModifier;
            PlayerController.Intelect -= oldItem.intelectModifier;
            PlayerController.Spirit -= oldItem.spiritModifier;
        }

        PlayerController.Agility += newItem.agilityModifier;
        PlayerController.Strength += newItem.strengthModifier;
        PlayerController.Stamina += newItem.staminaModifier;
        PlayerController.Intelect += newItem.intelectModifier;
        PlayerController.Spirit += newItem.spiritModifier;

        PlayerController.UpdateStats();

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        equipmentSlots[slotIndex].GetComponent<EquipmentSlots>().item = newItem;
        equipmentSlots[slotIndex].GetComponent<EquipmentSlots>().icon.sprite = newItem.icon;
        equipmentSlots[slotIndex].GetComponent<EquipmentSlots>().icon.gameObject.SetActive(true);
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (itemsTestWithAmount[i].item == item)
            {
                if(itemsTestWithAmount[i].amount > amount)
                {
                    ItemSlotWithAmount removeItem = new ItemSlotWithAmount();
                    removeItem.item = item;
                    removeItem.amount = itemsTestWithAmount[i].amount - amount;
                    removeItem.itemInSlot = true;

                    itemsTestWithAmount[i] = removeItem;
                }
                else
                {
                    itemsTestWithAmount.Remove(itemsTestWithAmount[i]);
                }
                break;
            }
        }

        Debug.Log("RemoveCalled" + item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    void UpdateUI()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsTestWithAmount.Count)
            {
                slots[i].amountOfItems = itemsTestWithAmount[i].amount;
                slots[i].AddItem(itemsTestWithAmount[i].item);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void OnLevelWasLoaded()
    {
        Init();
    }

    public bool CheckIfItemAmountExists(Item item, int amount)
    {
        Debug.Log(itemsTestWithAmount.Count);
        if(itemsTestWithAmount.Count == 0)
        {
            return false;
        }
        for (int i = 0; i < itemsTestWithAmount.Count; i++)
        {
            if (itemsTestWithAmount[i].itemInSlot) { 
                if (!itemsTestWithAmount[i].item)
                {
                    return false;
                }
                if (itemsTestWithAmount[i].item == item && itemsTestWithAmount[i].amount >= amount)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        return false;   
    }
}
