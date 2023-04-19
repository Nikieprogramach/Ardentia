using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLootController : MonoBehaviour
{
    public static DropLootController instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject coin;
    public GameObject[] dropItems;

    public void DropItem(Transform position)
    {
        if (Random.Range(0, 101) == 100)
        {
            Instantiate(dropItems[Random.Range(0, 11)], position.position, position.rotation);
        }
        else
        {
            Instantiate(coin, position.position, position.rotation);
        }


    }
}
