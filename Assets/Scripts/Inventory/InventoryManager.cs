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
    public Button useButton;
    public GameObject itemButtonsParent;

    private int selectedIndex;
    private Button[] itemButtons;
    private PlayerStatus playerStat;
    private List<ItemObject> localItemList;
    private Button tempButton;
    private InventoryItemHolder tempHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItemInInventory(int index)
    {

    }

    public void UseItem()
    {

    }

    private void SetupButtons()
    {

    }

    private void SetItem(InventoryItemHolder holder)
    {

    }

    private void ResetButtons(int count)
    {

    }
}
