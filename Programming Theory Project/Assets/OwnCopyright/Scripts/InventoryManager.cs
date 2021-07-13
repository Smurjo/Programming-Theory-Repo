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
    [SerializeField] GameObject buyButtonPrefab;
    [SerializeField] GameObject sellButtonPrefab;
    [SerializeField] private ItemSO moneyItem;
    [SerializeField] private int centsNeededToWin = 24000;//in cents

    private GameObject buyPanel;
    private GameObject sellPanel;
    public void Initialize(GameObject mainCanvas)
    {
        buyPanel = Instantiate(buyPanelPrefab, mainCanvas.transform);
        buyPanel.SetActive(false);
        sellPanel = Instantiate(sellPanelPrefab, mainCanvas.transform);
        sellPanel.SetActive(false);
        updateInventoryScreen(buyPanel, buyButtonPrefab);
        updateInventoryScreen(sellPanel, sellButtonPrefab);

        gameEvents.clearInventoryEvent += onClearInventory;
        gameEvents.itemAmountChangedEvent += onItemAmountChangedEvent;
        gameEvents.playerWantsToBuyEvent += ShowBuyScreen;
        gameEvents.buyScreenHideRequestedEvent += HideBuyScreen;
        gameEvents.buyScreenUpdateRequestedEvent += UpdateBuyScreen;
        gameEvents.playerWantsToSellEvent += ShowSellScreen;
        gameEvents.sellScreenUpdateRequestedEvent += UpdateSellScreen;
        gameEvents.sellScreenHideRequestedEvent += HideSellScreen;
        gameEvents.windowSizeChangedEvent += OnWindowSizechanged;
    }
    public void CleanUp()
    {
        gameEvents.clearInventoryEvent -= onClearInventory;
        gameEvents.itemAmountChangedEvent -= onItemAmountChangedEvent;
        gameEvents.playerWantsToBuyEvent -= ShowBuyScreen;
        gameEvents.buyScreenUpdateRequestedEvent -= UpdateBuyScreen;
        gameEvents.buyScreenHideRequestedEvent -= HideBuyScreen;
        gameEvents.playerWantsToSellEvent -= ShowSellScreen;
        gameEvents.sellScreenUpdateRequestedEvent -= UpdateSellScreen;
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
        updateInventoryScreen(buyPanel, buyButtonPrefab);
        updateInventoryScreen(sellPanel, sellButtonPrefab);
        if (item.Equals(moneyItem))
        {
            if (item.amountInInventory > centsNeededToWin)
            {
                gameEvents.raiseGameWonEvent();
                buyPanel.SetActive(false);
                sellPanel.SetActive(false);
            }
        }
    }
    public void UpdateBuyScreen()
    {
        updateInventoryScreen(buyPanel, buyButtonPrefab);
    }
    public void UpdateSellScreen()
    {
        updateInventoryScreen(sellPanel, sellButtonPrefab);
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

    public void OnWindowSizechanged()
    {
        updateInventoryScreen(buyPanel, buyButtonPrefab);
        updateInventoryScreen(sellPanel, sellButtonPrefab);
    }
    //utility
    public void updateInventoryScreen(GameObject panel, GameObject buttonPrefab)
    {
        int numberOfIconsPerRow = 1;
        int iconIndex = 0;
        Debug.Log("InventoryManager: updateInventoryScreen ");
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        RectTransform itemRectTransform = buttonPrefab.GetComponent<RectTransform>();
        if ((panelRectTransform != null) && (itemRectTransform != null))
        {
            GameObject closeButton = panelRectTransform.GetChild(0).gameObject;
            for (int i = panelRectTransform.childCount - 1; i > 1; i--)
            {
                Destroy(panelRectTransform.GetChild(i).gameObject);
            }
            panelRectTransform.DetachChildren();
            closeButton.transform.parent = panelRectTransform;
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
            //if (itemList.getItemAt(i).amountInInventory > 0)
            //{
            int iconRow = iconIndex / numberOfIconsPerRow;
            int iconColumn = iconIndex - iconRow * numberOfIconsPerRow;
            GameObject instantiatedItemIcon = Instantiate(buttonPrefab, panel.transform);
            RectTransform panelTransform = (RectTransform)panel.transform;
            Vector3[] worldCorners = new Vector3[4];
            panelTransform.GetWorldCorners(worldCorners);
            //  Debug.Log("InventoryManager: updateInventoryScreen panel top left:" + worldCorners[1]);
            itemRectTransform = instantiatedItemIcon.GetComponent<RectTransform>();
            itemRectTransform.position = new Vector3(
             worldCorners[1].x + pixelOffsetX + (2 * iconColumn + 1) * itemFrameWidth + iconColumn * itemRectTransform.rect.width,
                panelRectTransform.rect.height - pixelOffsetY - (2 * iconRow + 1) * itemFrameWidth - iconRow * itemRectTransform.rect.height, 0);
            iconIndex++;
            InventoryButtonScript buttonScript = instantiatedItemIcon.GetComponent<InventoryButtonScript>();
            buttonScript.item = itemList.getItemAt(i);
            buttonScript.updateAmount();
            buttonScript.updateIcon();
            buttonScript.updatePrice();
            instantiatedItemIcon.GetComponent<Image>().sprite = itemList.getItemAt(i).icon;
            // }
        }
    }
}
