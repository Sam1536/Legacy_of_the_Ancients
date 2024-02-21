using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject inventoryButton;

    public GameObject itemPrefabs;

    public static GameController Instance;

    void Awake()
    {
        Instance = this;
    }
    
    public void ActiveInventory(GameObject GO)
    {
        GO.SetActive(true);
        inventoryButton.SetActive(false);
    }

    public void DisableInventory(GameObject GO)
    {
        GO.SetActive(false);
        inventoryButton.SetActive(true);
    }

}


