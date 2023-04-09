using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int agilityModifier;
    public int strengthModifier;
    public int staminaModifier;
    public int intelectModifier;
    public int spiritModifier;

    public override void Use()
    {
        base.Use();
        Inventory.instance.Equip(this);
        RemoveFromInventory();
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Boots, Trinket, Ring, Amulet, Weapon }