using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class BuyButtonScript : InventoryButtonScript
    {
        private bool playerHasBoughtOne;
        private void Start()
        {
            playerHasBoughtOne = false;
        }
        public override void updatePrice()
        {
            Text priceText = gameObject.transform.Find("PriceText").gameObject.GetComponent<Text>();
            string text = item.buyPrice.ToString();
            if (text.Length > 2)
            {
                priceText.text = text.Substring(0, text.Length - 2)
                + "." + text.Substring(text.Length - 2, 2);
            }
            else
            {
                priceText.text = "0.00".Substring(0, 4 - text.Length) + text;
            }
        }
        public void itemBought()
        {
            Debug.Log("BuyButtonScript: itemBought");
            if (money.amountInInventory >= item.buyPrice)
            {
                playerHasBoughtOne = true;
                gameEvents.raiseItemAmountChangedEvent(item, 1);
                gameEvents.raiseItemAmountChangedEvent(money, -item.buyPrice);
                gameEvents.raiseBuyScreenUpdateRequestedEvent();
            }
        }
        public void itemOrderCanceled()
        {
            Debug.Log("BuyButtonScript: Order canceled");
            if (playerHasBoughtOne)
            {
                gameEvents.raiseItemAmountChangedEvent(item, -1);
                gameEvents.raiseItemAmountChangedEvent(money, item.buyPrice);
                gameEvents.raiseBuyScreenUpdateRequestedEvent();
                playerHasBoughtOne = false;
            }
        }
    }
}
