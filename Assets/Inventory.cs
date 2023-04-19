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

    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                return false;
            }

            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
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
            AddItem(oldItem);

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

    public void RemoveItem(Item item)
    {
        items.Remove(item);

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
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
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
}
