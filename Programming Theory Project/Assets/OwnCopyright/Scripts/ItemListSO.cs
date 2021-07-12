using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemListSO", menuName = "ScriptableObjects/ItemList", order = 1)]

public class ItemListSO : ScriptableObject
{
    [SerializeField] List<ItemSO> items;

    public int PossibleItemsCount() {
        return items.Count;
    }

    public ItemSO getItemAt(int index)
    {
        return items[index];
    }
}
