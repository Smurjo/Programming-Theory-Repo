using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInit : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] InventoryManager inventoryManager;

    public void OnEnable()
    {
        inventoryManager.onInitialization(mainCanvas);
    }
    public void OnDisable()
    {
        inventoryManager.onCleanup();
    }
}
