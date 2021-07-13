using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PriceMaster", menuName = "ScriptableObjects/PriceMaster")]

public class PriceMaster : ScriptableObject
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private float minBuyPriceFactor = 0.7f;
    [SerializeField] private float maxBuyPriceFactor = 1.5f;
    [SerializeField] private float minSellPriceFactor= 0.5f;
    [SerializeField] private float maxSellPriceFactor = 1.3f;

    public void Initialize()
    {
        UpdateBuyPrices();
        UpdateSellPrices();
        gameEvents.playerWantsToBuyEvent += UpdateSellPrices;//updatePrices when player isn't looking
        gameEvents.playerWantsToSellEvent += UpdateBuyPrices;//updatePrices when player isn't looking
    }

    public void CleanUp()
    {
        gameEvents.playerWantsToBuyEvent -= UpdateSellPrices;//updatePrices when player isn't looking
        gameEvents.playerWantsToSellEvent -= UpdateBuyPrices;//updatePrices when player isn't looking
    }

    public void UpdateBuyPrices()
    {
        for (int i = 0; i < itemList.PossibleItemsCount(); i++)
        {
            ItemSO item = itemList.getItemAt(i);
            item.buyPrice = Mathf.RoundToInt(item.basePrice * Random.Range(minBuyPriceFactor, maxBuyPriceFactor));
        }
        gameEvents.raiseBuyScreenUpdateRequestedEvent();
    }
    public void UpdateSellPrices()
    {
        for (int i = 0; i < itemList.PossibleItemsCount(); i++)
        {
            ItemSO item = itemList.getItemAt(i);
            item.sellPrice = Mathf.RoundToInt(item.basePrice * Random.Range(minSellPriceFactor, maxSellPriceFactor));
        }
        gameEvents.raiseSellScreenUpdateRequestedEvent();
    }
}
