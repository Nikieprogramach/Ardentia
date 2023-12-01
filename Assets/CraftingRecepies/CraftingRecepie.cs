using UnityEngine;

[CreateAssetMenu(fileName = "New CraftingRecepie", menuName = "Crafting/CreaftingRecepie")]
public class CraftingRecepie : ScriptableObject
{
    public Item product;
    public int amountOfProduct;


    public Item ingredient1;
    public int amountOfIngredient1;

    public Item ingredient2;
    public int amountOfIngredient2;

    public Item ingredient3;
    public int amountOfIngredient3;

    public Item ingredient4;
    public int amountOfIngredient4;
}
