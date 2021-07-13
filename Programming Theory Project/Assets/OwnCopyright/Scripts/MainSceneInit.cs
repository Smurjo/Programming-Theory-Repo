using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInit : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] MoneyMaster moneyMaster;
    [SerializeField] PriceMaster priceMaster;

    public void OnEnable()
    {
        inventoryManager.Initialize(mainCanvas);
        moneyMaster.Initialize(mainCanvas);
        priceMaster.Initialize();
    }
    public void OnDisable()
    {
        inventoryManager.CleanUp(); 
        moneyMaster.CleanUp();
        priceMaster.CleanUp();
    }
}
