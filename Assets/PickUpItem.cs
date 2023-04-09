using UnityEngine;

public class PickUpItem : Interactable
{
    public Item item;

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
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }

    }
}