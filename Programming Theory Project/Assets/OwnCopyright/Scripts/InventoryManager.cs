using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryManager", menuName = "ScriptableObjects/InventoryManager")]
public class InventoryManager : ScriptableObject
{
    [SerializeField] GameEvents gameEvents;
    [SerializeField] ItemListSO itemList;
    [SerializeField] int itemFrameWidth;//minimum free number of pixels around item
    [SerializeField] int pixelOffsetX;
    [SerializeField] int pixelOffsetY;
    [SerializeField] GameObject buyPanelPrefab;
    [SerializeField] GameObject sellPanelPrefab;
    [SerializeField] GameObject buyBottonPrefab;
    [SerializeField] GameObject sellBottonPrefab;

    private GameObject buyPanel;
    private GameObject sellPanel;
    public void onInitialization(GameObject mainCanvas)
    {
        buyPanel = Instantiate(buyPanelPrefab, mainCanvas.transform);
        buyPanel.SetActive(false);
        sellPanel = Instantiate(sellPanelPrefab, mainCanvas.transform);
        sellPanel.SetActive(false);

        gameEvents.clearInventoryEvent += onClearInventory;
        gameEvents.itemAmountChangedEvent += onItemAmountChangedEvent;
        gameEvents.playerWantsToBuyEvent += ShowBuyScreen;
        gameEvents.buyScreenHideRequestedEvent += HideBuyScreen;
        gameEvents.playerWantsToSellEvent += ShowSellScreen;
        gameEvents.sellScreenHideRequestedEvent += HideSellScreen;
    }
    public void onCleanup()
    {
        gameEvents.clearInventoryEvent -= onClearInventory;
        gameEvents.itemAmountChangedEvent -= onItemAmountChangedEvent;
        gameEvents.playerWantsToBuyEvent -= ShowBuyScreen;
        gameEvents.buyScreenHideRequestedEvent -= HideBuyScreen;
        gameEvents.playerWantsToSellEvent -= ShowSellScreen;
        gameEvents.sellScreenHideRequestedEvent -= HideSellScreen;
    }
    public void onClearInventory()
    {
        for (int i = 0; i < itemList.PossibleItemsCount(); i++)
        {
            itemList.getItemAt(i).amountInInventory = 0;
        }
    }
    public void onItemAmountChangedEvent(ItemSO item, int amount)
    {
        //Debug.Log("InventoryManager: reacting to item amount changed event");
        item.amountInInventory += amount;
        updateInventoryScreen();
    }

    public void onInventoryScreenUpdateRequestedEvent()
    {//Debug.Log("InventoryManager: reacting to InventoryScreenUpdateRequested event ");
        updateInventoryScreen();
    }
    public void onInventoryScreenDisplayRequestedEvent()
    {//Debug.Log("InventoryManager: reacting to InventoryScreenDisplayRequested event ");
        buyPanel.SetActive(true);
    }
    public void onInventoryScreenHideRequestedEvent()
    {//Debug.Log("InventoryManager: reacting to InventoryScreenDisplayRequested event ");

    }
    public void ShowBuyScreen()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
    }
    public void ShowSellScreen()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);
    }
    public void HideBuyScreen()
    {
        buyPanel.SetActive(false);
    }
    public void HideSellScreen()
    {
        sellPanel.SetActive(false);
    }
    //utility
    public void updateInventoryScreen()
    {
        int numberOfIconsPerRow = 1;
        int iconIndex = 0;
        Debug.Log("InventoryManager: updateInventoryScreen ");
        RectTransform panelRectTransform = buyPanel.GetComponent<RectTransform>();
        RectTransform itemRectTransform = sellBottonPrefab.GetComponent<RectTransform>();
        if ((panelRectTransform != null) && (itemRectTransform != null))
        {
            GameObject closeButton = panelRectTransform.GetChild(0).gameObject;
            GameObject emptyText = panelRectTransform.GetChild(1).gameObject;
            for (int i = panelRectTransform.childCount - 1; i > 1; i--)
            {
                Destroy(panelRectTransform.GetChild(i).gameObject);
            }
            panelRectTransform.DetachChildren();
            closeButton.transform.parent = panelRectTransform;
            emptyText.transform.parent = panelRectTransform;
            numberOfIconsPerRow = Mathf.FloorToInt(
                (panelRectTransform.rect.width) /
                (itemRectTransform.rect.width + 2 * itemFrameWidth));
            Debug.Log("InventoryManager: updateInventoryScreen numberOfIconsPerRow" + numberOfIconsPerRow);
        }
        else
        {
            Debug.Log("InventoryManager: updateInventoryScreen rectTransform = null");
        }
        for (int i = 0; i < itemList.PossibleItemsCount(); i++)
        {
            if (itemList.getItemAt(i).amountInInventory > 0)
            {
                int iconRow = iconIndex / numberOfIconsPerRow;
                int iconColumn = iconIndex - iconRow * numberOfIconsPerRow;
                Debug.Log("InventoryManager: updateInventoryScreen rectTransform = null");

                GameObject instantiatedItemIcon = Instantiate(sellBottonPrefab, buyPanel.transform);
                itemRectTransform = instantiatedItemIcon.GetComponent<RectTransform>();
                itemRectTransform.position = new Vector3(
                    pixelOffsetX + (2 * iconColumn + 1) * itemFrameWidth + iconColumn * itemRectTransform.rect.width,
                    panelRectTransform.rect.height - pixelOffsetY - (2 * iconRow + 1) * itemFrameWidth - iconRow * itemRectTransform.rect.height, 0);
                iconIndex++;
                InventoryButtonScript buttonScript = instantiatedItemIcon.GetComponent<InventoryButtonScript>();
                buttonScript.item = itemList.getItemAt(i);
                buttonScript.updateAmount();
                instantiatedItemIcon.GetComponent<Image>().sprite = itemList.getItemAt(i).icon;
            }
        }
    }
}
