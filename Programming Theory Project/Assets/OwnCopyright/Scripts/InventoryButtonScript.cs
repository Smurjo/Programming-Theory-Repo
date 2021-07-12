using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonScript : MonoBehaviour
{
    public ItemSO item;
  
    public void updateAmount() {
        Text amountText= gameObject.transform.Find("AmountText").gameObject.GetComponent<Text> ();
        amountText.text = item.amountInInventory.ToString();
    }

}
