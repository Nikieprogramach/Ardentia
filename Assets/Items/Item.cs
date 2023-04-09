using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int price;

    public virtual void Use()
    {
        Debug.Log("Using Item");
    }

}
