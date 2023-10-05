using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory;
    [YarnCommand("add_item")]
    public void AddItem(string newItem)
    {
        inventory.Add(newItem);
    }
    public void RemoveItem(string targetItem)
    {
        inventory.Remove(targetItem);
    }
}
