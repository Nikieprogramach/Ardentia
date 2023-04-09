using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        transform.position = Input.mousePosition;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
