using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public TMP_Text itemName;
    public TMP_Text itemType;
    public TMP_Text itemEffect;
    public TMP_Text money;
    public Button useButton;
    public GameObject itemButtonsParent;

    private int selectedIndex;
    private Button[] itemButtons;
    private PlayerStatus playerStat;
    private List<ItemObject> itemListReference;
    private Button tempButton;
    private InventoryItemHolder tempHolder;

    private Item tempItem = null;

    private void Awake()
    {
        itemButtons = itemButtonsParent.GetComponentsInChildren<Button>();
    }

    void OnEnable()
    {
        playerStat = GameManager.Instance.Blackboard.Player.playerStatus;
        itemListReference = playerStat.itemList;
        money.text = "Coins: " + playerStat.money;
        SetupButtons();
    }

    public void SelectItemInInventory(int index)
    {
        selectedIndex = index;
        tempItem = GameManager.Instance.Blackboard.ItemLibrary.GetItem(itemListReference[selectedIndex].itemID);
        itemName.text = tempItem.name;
        itemType.text = tempItem.GetTypeString();
        itemEffect.text = tempItem.text;
        if (tempItem.usable) useButton.interactable = true;
        else useButton.interactable = false;
        tempItem = null;
    }

    public void UseItem()
    {
        if (selectedIndex == -1) return;
        useButton.interactable = false;

        Debug.Log("using item, used item index: " + selectedIndex);

        var _item = GameManager.Instance.Blackboard.ItemLibrary.GetItem(itemListReference[selectedIndex].itemID);

        //item takes effect
        foreach (var effect in _item.effects)
        {
            if (effect.statID == 1)
            {
                playerStat.ModifyHealth(effect.value);
            }
            else if (effect.statID == 2)
            {
                playerStat.ModifyHunger(effect.value);
            }
        }

        //resolve item quantity
        itemListReference[selectedIndex].count--;
        if (itemListReference[selectedIndex].count <= 0)
        {
            ResetButtons(itemListReference.Count);
            itemListReference.RemoveAt(selectedIndex);
        }
        else
        {
            useButton.interactable = true;
        }

        GameManager.Instance.Blackboard.Player.playerStatus.itemList = itemListReference;   //probably dont need as the list is sent by ref
        SetupButtons();
    }

    private void SetupButtons()
    {
        for (int i = 0; i < itemListReference.Count; i++)
        {
            tempButton = itemButtons[i];
            tempButton.interactable = true;
            tempHolder = tempButton.GetComponent<InventoryItemHolder>();
            tempHolder.item = itemListReference[i];
            tempHolder.inventoryIndex = i;
            SetItem(tempHolder);
        }
    }

    private void SetItem(InventoryItemHolder holder)
    {
        tempItem = GameManager.Instance.Blackboard.ItemLibrary.GetItem(holder.item.itemID);
        holder.itemImage.sprite = tempItem.icon;
        holder.itemImage.enabled = true;
        if (tempItem.stackable) holder.itemCount.text = holder.item.count.ToString();
        tempItem = null;
    }

    private void ResetButtons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            itemButtons[i].interactable = false;
            itemButtons[i].GetComponent<InventoryItemHolder>().ResetFields();
        }
    }
}
