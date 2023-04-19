using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public static ItemInfo instance;

    void Awake()
    {
        instance = this;
    }
    public GameObject ItemInfoShowup;
    public bool isShowed = false;

    public Text name;
    public Text description;

    public GameObject stats;

    public Text agility;
    public Text strength;
    public Text stamina;
    public Text intelect;
    public Text spirit;

    void Update()
    {
        if(isShowed)
        {
            float pivotX = Input.mousePosition.x/ Screen.width;
            float pivotY = Input.mousePosition.y / Screen.height;

            transform.GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);

            transform.position = Input.mousePosition;
        }
    }

    public void ShowInfo(Item item)
    {
        isShowed = true;
        ItemInfoShowup.SetActive(true);
        name.text = item.name;
        description.text = item.description;
        
        if(item.hasStats == true)
        {
            stats.SetActive(true);

            agility.text = "Agility: " + item.agilityModifier;
            strength.text = "Strength: " + item.strengthModifier;
            stamina.text = "Stamina: " + item.staminaModifier;
            intelect.text = "Intelect: " + item.intelectModifier;
            spirit.text = "Spirit: " + item.spiritModifier;
        }
        else
        {
            stats.SetActive(false);
            //transform.GetComponent<RectTransform>().rect.size = new Vector2(100, 50);
        }


    }

    public void HideInfo()
    {
        isShowed = false;
        ItemInfoShowup.SetActive(false);
    }
}
