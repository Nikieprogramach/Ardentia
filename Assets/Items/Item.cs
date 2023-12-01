using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isStackable = false;
    public int price;

    public bool hasStats = false;

    public int agilityModifier;
    public int strengthModifier;
    public int staminaModifier;
    public int intelectModifier;
    public int spiritModifier;
    public bool IsRanged = false;

    public virtual void Use()
    {
        Debug.Log("Using Item");
    }

}
