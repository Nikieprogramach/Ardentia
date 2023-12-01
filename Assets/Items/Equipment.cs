using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    //public int agilityModifier;
    //public int strengthModifier;
    //public int staminaModifier;
    //public int intelectModifier;
    //public int spiritModifier;
    //public bool IsRanged = false;

    public override void Use()
    {
        base.Use();
        Inventory.instance.Equip(this);
        RemoveFromInventory();
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this, 1);
    }
}

public enum EquipmentSlot { Head, Chest, Hands, Boots, Trinket, Ring, Amulet, Weapon }