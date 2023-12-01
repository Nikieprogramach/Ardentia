using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public float regenHealth;
    public float regenMana;

    public override void Use()
    {
        base.Use();
        PlayerController.instance.UsePotion(this);
        RemoveFromInventory();
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this, 1);
    }
}
