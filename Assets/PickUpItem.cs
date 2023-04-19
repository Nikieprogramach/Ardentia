using UnityEngine;

public class PickUpItem : Interactable
{
    public Item item;
    public Equipment equipment;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting");
        float distance = Vector2.Distance(PlayerController.instance.GetComponent<Transform>().position, PlayerController.instance.GetComponent<Transform>().position);
        if (distance <= radius)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        if(item != null)
        {
            bool wasPickedUp = Inventory.instance.AddItem(item);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }else if(equipment != null)
        {
            bool wasPickedUp = Inventory.instance.AddItem(equipment);
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}