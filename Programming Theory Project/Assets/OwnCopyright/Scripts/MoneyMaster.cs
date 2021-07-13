using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MoneyMaster", menuName = "ScriptableObjects/MoneyMaster")]
public class MoneyMaster : ScriptableObject
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private ItemSO moneyItem;
 
    private Text moneyText;
    int lastMoney;

    public void Initialize(GameObject canvas)
    {
        moneyText = canvas.transform.Find("MoneyText").gameObject.GetComponent<Text>();
        lastMoney = moneyItem.amountInInventory;
        moneyText.text = MoneyToString(lastMoney);
        gameEvents.itemAmountChangedEvent += UpdateMoneyText;
    }

    public void CleanUp()
    {
        gameEvents.itemAmountChangedEvent -= UpdateMoneyText;
    }
    public void UpdateMoneyText(ItemSO item, int amount)
    {
        if (item.Equals(moneyItem))
        {
            lastMoney += amount;//since I can't know whether the change is already booked in the money
            moneyText.text = MoneyToString(lastMoney);
        }
        else
        {
            lastMoney = moneyItem.amountInInventory;//get the actual money when there is no change
        }
    }
    //Utility
    public static string MoneyToString(int moneyInCent)
    {
        string text = moneyInCent.ToString();
        if (text.Length > 2)
        {
            return text.Substring(0, text.Length - 2) + "." + text.Substring(text.Length - 2, 2);
        }
        else
        {
            return "0.00".Substring(0, 4 - text.Length) + text;
        }
    }
}
