using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class BuyButtonScript : InventoryButtonScript
    {
        public override void updatePrice()
        {
            Text priceText = gameObject.transform.Find("PriceText").gameObject.GetComponent<Text>();
            priceText.text = MoneyMaster.MoneyToString(item.buyPrice);
        }
        public void itemBought()
        {
            Debug.Log("BuyButtonScript: itemBought");
            if (money.amountInInventory >= item.buyPrice)
            {
                gameEvents.raiseItemAmountChangedEvent(item, 1);
                gameEvents.raiseItemAmountChangedEvent(money, -item.buyPrice);
                gameEvents.raiseBuyScreenUpdateRequestedEvent();
            }
        }
    }
}
