using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class SellButtonScript : InventoryButtonScript
    {
        public override void updatePrice()
        {
            Text priceText = gameObject.transform.Find("PriceText").gameObject.GetComponent<Text>();
            priceText.text = MoneyMaster.MoneyToString(item.sellPrice);
        }

        public void itemSold()
        {
            if (item.amountInInventory>0)
            {
                Debug.Log("SellButtonScript: itemSold");
                gameEvents.raiseItemAmountChangedEvent(item, -1);
                gameEvents.raiseItemAmountChangedEvent(money, item.sellPrice);
                gameEvents.raiseSellScreenUpdateRequestedEvent(); 
            }
        }
    }
}
