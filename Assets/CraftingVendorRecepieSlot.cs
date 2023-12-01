using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingVendorRecepieSlot : MonoBehaviour
{
    public GameObject Player;

    public CraftingRecepie craftingRecepie;

    public Image product;
    public Text amountOfProduct;

    public Image ingredient1;
    public Text amountOfIngredient1;

    public Image ingredient2;
    public Text amountOfIngredient2;

    public Image ingredient3;
    public Text amountOfIngredient3;

    public Image ingredient4;
    public Text amountOfIngredient4;


    public GameObject itemInfoObj;

    void Start()
    {
        Init();
    }

    void Init()
    {
        itemInfoObj = ItemInfo.instance.gameObject;
    }

    public void CraftItem()
    {
        if (Inventory.instance.CheckIfItemAmountExists(craftingRecepie.ingredient1, craftingRecepie.amountOfIngredient1))
        {
            if (craftingRecepie.ingredient2 != null && Inventory.instance.CheckIfItemAmountExists(craftingRecepie.ingredient2, craftingRecepie.amountOfIngredient2))
            {
                if (craftingRecepie.ingredient3 != null && Inventory.instance.CheckIfItemAmountExists(craftingRecepie.ingredient3, craftingRecepie.amountOfIngredient3))
                {
                    if (craftingRecepie.ingredient4 != null && Inventory.instance.CheckIfItemAmountExists(craftingRecepie.ingredient4, craftingRecepie.amountOfIngredient4))
                    {
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient1, craftingRecepie.amountOfIngredient1);
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient2, craftingRecepie.amountOfIngredient2);
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient3, craftingRecepie.amountOfIngredient3);
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient4, craftingRecepie.amountOfIngredient4);
                        Inventory.instance.AddItem(craftingRecepie.product, craftingRecepie.amountOfProduct);
                    }
                    else
                    {
                        if (craftingRecepie.ingredient4 == null)
                        {
                            Inventory.instance.RemoveItem(craftingRecepie.ingredient1, craftingRecepie.amountOfIngredient1);
                            Inventory.instance.RemoveItem(craftingRecepie.ingredient2, craftingRecepie.amountOfIngredient2);
                            Inventory.instance.RemoveItem(craftingRecepie.ingredient3, craftingRecepie.amountOfIngredient3);
                            Inventory.instance.AddItem(craftingRecepie.product, craftingRecepie.amountOfProduct);
                        }
                        else
                        {
                            // Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient4.name + "!");
                            Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient4.name + " to craft " + craftingRecepie.product.name + "!");
                            Debug.Log("Not enough " + ingredient4);
                        }

                    }
                }
                else
                {
                    if (craftingRecepie.ingredient3 == null)
                    {
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient1, craftingRecepie.amountOfIngredient1);
                        Inventory.instance.RemoveItem(craftingRecepie.ingredient2, craftingRecepie.amountOfIngredient2);
                        Inventory.instance.AddItem(craftingRecepie.product, craftingRecepie.amountOfProduct);
                    }
                    else
                    {
                        // Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient3.name + "!");
                        Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient3.name + " to craft " + craftingRecepie.product.name + "!");
                        Debug.Log("Not enough " + ingredient3);
                    }
                }
            }
            else
            {
                if(craftingRecepie.ingredient2 == null)
                {
                    Inventory.instance.RemoveItem(craftingRecepie.ingredient1, craftingRecepie.amountOfIngredient1);
                    Inventory.instance.AddItem(craftingRecepie.product, craftingRecepie.amountOfProduct);
                }
                else
                {
                    // Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient2.name + "!");
                    Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient2.name + " to craft " + craftingRecepie.product.name + "!");
                    Debug.Log("Not enough " + ingredient2);
                }
            }
        }
        else
        {
            // Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient1.name + "!");
            Player.GetComponent<PlayerController>().SetPopup("Not enough " + craftingRecepie.ingredient1.name + " to craft " + craftingRecepie.product.name + "!");
            Debug.Log("Not enough " + ingredient1);
        }
    }

    public void OnMouseOver(string slot)
    {
        if(slot == "i1")
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(craftingRecepie.ingredient1);
        }
        else if (slot == "i2")
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(craftingRecepie.ingredient2);
        }
        else if (slot == "i3")
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(craftingRecepie.ingredient3);
        }
        else if (slot == "i4")
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(craftingRecepie.ingredient4);
        }
        else if (slot == "p")
        {
            itemInfoObj.GetComponent<ItemInfo>().ShowInfo(craftingRecepie.product);
        }
    }
    
    public void OnMouseExit()
    {
        itemInfoObj.GetComponent<ItemInfo>().HideInfo();
    }

    public void SetSlot(CraftingRecepie recepie, GameObject player)
    {
        Player = player;
        Debug.Log(gameObject + " " + recepie);
        craftingRecepie = recepie;

        product.gameObject.SetActive(true);
        product.sprite = recepie.product.icon;
        //product.sprite = recepie.ingredient1.icon;
        if(recepie.amountOfProduct > 1)
        {
            amountOfIngredient1.gameObject.SetActive(true);
            amountOfProduct.text = recepie.amountOfProduct.ToString();
        }
        

        if (recepie.ingredient1 != null)
        {
            ingredient1.gameObject.SetActive(true);
            ingredient1.sprite = recepie.ingredient1.icon;
            if(recepie.amountOfIngredient1 > 1)
            {
                amountOfIngredient1.gameObject.SetActive(true);
                amountOfIngredient1.text = recepie.amountOfIngredient1.ToString();
            }
        }


        if (recepie.ingredient2 != null)
        {
            ingredient2.gameObject.SetActive(true);
            ingredient2.sprite = recepie.ingredient2.icon;
            if (recepie.amountOfIngredient2 > 1)
            {
                amountOfIngredient2.gameObject.SetActive(true);
                amountOfIngredient2.text = recepie.amountOfIngredient2.ToString();
            }
        }


        if (recepie.ingredient3 != null)
        {
            ingredient3.gameObject.SetActive(true);
            ingredient3.sprite = recepie.ingredient3.icon;
            if (recepie.amountOfIngredient3 > 1)
            {
                amountOfIngredient3.gameObject.SetActive(true);
                amountOfIngredient3.text = recepie.amountOfIngredient3.ToString();
            }
        }


        if (recepie.ingredient4 != null)
        {
            ingredient4.gameObject.SetActive(true);
            ingredient4.gameObject.SetActive(true);
            ingredient4.sprite = recepie.ingredient4.icon;
            if (recepie.amountOfIngredient4 > 1)
            {
                amountOfIngredient4.gameObject.SetActive(true);
                amountOfIngredient4.text = recepie.amountOfIngredient4.ToString();
            }
        }

    }
}
