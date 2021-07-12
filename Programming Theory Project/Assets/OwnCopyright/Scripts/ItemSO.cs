using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Item")]
   
public class ItemSO : ScriptableObject {
    public int amountInInventory;
    public float weightPerUnit; //weight in kg
    public string itemName;
    public Sprite icon;
    public string description;
}

