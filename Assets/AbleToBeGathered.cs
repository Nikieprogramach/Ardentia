using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleToBeGathered : MonoBehaviour
{
    public string typeOfResource;

    public GameObject wood;
    public GameObject stone;

    public void Gather()
    {
        if(typeOfResource == "Wood")
        {
            Instantiate(wood, transform.position, transform.rotation);
        }
        else if(typeOfResource == "Stone")
        {
            Instantiate(stone, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
