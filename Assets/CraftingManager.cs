using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public Item currentItem;
    public Image customCursor;

    public GameObject pickedUpSlot;

    public GameObject[] Slots;

    public List<Item> itemList;
    public string[] craftingRecepies;
    public Item[] recepieResults;

    public CraftingSlot resultSlot;

    public Item oldItem;

    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {

            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                GameObject nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (GameObject slot in Slots)
                {
                    float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if(distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSlot = slot;
                    }
                }

                if(nearestSlot.GetComponent<CraftingSlot>() != null)
                {
                    //if(nearestSlot.GetComponent<CraftingSlot>().item == null)
                    //{
                        nearestSlot.GetComponent<CraftingSlot>().icon.gameObject.SetActive(true);
                        nearestSlot.GetComponent<CraftingSlot>().icon.sprite = currentItem.icon;
                        nearestSlot.GetComponent<CraftingSlot>().item = currentItem;

                        itemList[nearestSlot.GetComponent<CraftingSlot>().slotIndex] = currentItem;

                        oldItem = currentItem;

                        currentItem = null;
                        customCursor.GetComponent<CustomCursor>().item = null;
                    //}
                    //else
                    //{
                        //Debug.Log("old item is" + oldItem);
                        //Debug.Log("current item is" + currentItem);

                        //nearestSlot.GetComponent<CraftingSlot>().icon.gameObject.SetActive(true);
                        //nearestSlot.GetComponent<CraftingSlot>().icon.sprite = currentItem.icon;
                        //nearestSlot.GetComponent<CraftingSlot>().item = currentItem;

                        //oldItem = currentItem;

                        //customCursor.gameObject.SetActive(true);
                        //customCursor.GetComponent<CustomCursor>().item = oldItem;
                        //customCursor.GetComponent<Image>().sprite = oldItem.icon;

                        //currentItem = oldItem;

                    //}



                CheckForCompletedRecepies();
                }
                else
                {
                    Inventory.instance.AddItem(currentItem);
                    currentItem = null;
                    customCursor.GetComponent<CustomCursor>().item = null;
                }
            }

            if(pickedUpSlot != null && pickedUpSlot.GetComponent<InventorySlot>().canRemoveItems == false)
            {
                pickedUpSlot.GetComponent<InventorySlot>().canRemoveItems = true;
            }
        }
    }

    public void CheckForCompletedRecepies()
    {
        resultSlot.icon.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecepieString = "";

        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecepieString += item.name;
            }else if(item == null)
            {
                currentRecepieString += "null";
            }
        }
        for (int i = 0; i < craftingRecepies.Length; i++)
        {
            if (craftingRecepies[i] == currentRecepieString)
            {
                resultSlot.icon.gameObject.SetActive(true);
                resultSlot.icon.sprite = recepieResults[i].icon;
                resultSlot.item = recepieResults[i];
            }
        }
    }
}
