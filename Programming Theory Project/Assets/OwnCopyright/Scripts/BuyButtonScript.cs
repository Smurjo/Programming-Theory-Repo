using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class BuyButtonScript : InventoryButtonScript // INHERITANCE
    {
        public override void updatePrice() // POLYMORPHISM
        {
            Text priceText = gameObject.transform.Find("PriceText").gameObject.GetComponent<Text>();
            priceText.text = MoneyMaster.MoneyToString(item.buyPrice);// ABSTRACTION
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
