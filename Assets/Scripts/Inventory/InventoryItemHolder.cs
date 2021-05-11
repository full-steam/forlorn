using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHolder : MonoBehaviour
{

    public ItemObject item;
    public int inventoryIndex;

    public Image itemImage;
    public TMP_Text itemCount;

    public void ClickItemInInventory()
    {
        GetComponentInParent<InventoryManager>().SelectItemInInventory(inventoryIndex);
    }

    public void ResetFields()
    {
        itemImage.enabled = false;
        itemCount.text = "";
        item = new ItemObject();
    }
}
