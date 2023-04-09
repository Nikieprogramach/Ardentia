using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] itemsInChest;
    public Transform spownPoint;
    private bool isOpened = false;
    
    public void OpenChest()
    {
        if (!isOpened)
        {
            transform.GetComponent<Animator>().SetBool("IsOpened", true);

            foreach(GameObject item in itemsInChest)
            {
                Instantiate(item, spownPoint.position, spownPoint.rotation);
            }
            isOpened = true;
        }
    }
}
