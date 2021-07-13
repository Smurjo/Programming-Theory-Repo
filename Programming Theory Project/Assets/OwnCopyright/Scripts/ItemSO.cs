using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Item")]
   
public class ItemSO : ScriptableObject {
    public int amountInInventory;
    public int sellPrice;//price in cent
    public int buyPrice;//price in cent
    public int basePrice;//price in cent, from which the other prices will be derived
    public float weightPerUnit; //weight in kg
    public string itemName;
    public Sprite icon;
    public string description;
}

