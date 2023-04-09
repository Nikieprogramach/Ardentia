using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEnter : MonoBehaviour
{
    public string houseName;

    Vector2 zeroZero;

    public PlayerController PlayerController;

    void Start()
    {
        Init();
    }

    void Init()
    {
        PlayerController = PlayerController.instance;

        zeroZero.x = 0;
        zeroZero.y = 0;
    }

    public void EnterHouse()
    {
        PlayerController.GetComponent<Transform>().position = zeroZero;
        Debug.Log("Entering house");
        SceneManager.LoadScene(houseName);
    }
}
