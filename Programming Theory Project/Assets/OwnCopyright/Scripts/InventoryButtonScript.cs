using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonScript : MonoBehaviour
{
    [SerializeField] public ItemSO item;
    [SerializeField] protected ItemSO money;
    [SerializeField] protected GameEvents gameEvents;

    public virtual void updateAmount() 
    {
        Text amountText= gameObject.transform.Find("AmountText").gameObject.GetComponent<Text> ();
        amountText.text = item.amountInInventory.ToString();
    }
    public virtual void updateIcon()
    {
        Image icon = gameObject.transform.Find("Button").gameObject.GetComponent<Image>();
        icon.sprite = item.icon;
    }
    public virtual void updatePrice()
    { }
    }
