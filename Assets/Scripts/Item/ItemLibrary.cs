using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary
{

    private Dictionary<int, Item> library = new Dictionary<int, Item>();
    private Item[] items;

    public ItemLibrary()
    {
        SetLibrary();
    }

    private void SetLibrary()
    {
        items = Resources.LoadAll<Item>("Item");

        //probably needs to check for duplicates just in case?

        foreach (var item in items)
        {
            library.Add(item.id, item);
        }
    }

    public Item GetItem(int id)
    {
        return library[id];
    }
}
