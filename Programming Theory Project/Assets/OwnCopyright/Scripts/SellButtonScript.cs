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
            string text = item.sellPrice.ToString();
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

        public void itemSold()
        {
            Debug.Log("SellButtonScript: itemSold");
            gameEvents.raiseItemAmountChangedEvent(item, -1);
            gameEvents.raiseItemAmountChangedEvent(money, item.sellPrice);
            gameEvents.raiseSellScreenUpdateRequestedEvent();
        }
    }
}
