using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class ItemCollection<TItemCollectionFilter, TItemCollection, TItem> : Saveable<TItemCollection>
    where TItemCollectionFilter : ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>
    where TItemCollection : ItemCollection<TItemCollectionFilter, TItemCollection, TItem>
    where TItem : Item<TItem>
{
    public EnumSetting rarities;
    public EnumSetting books;
    public TItemCollectionFilter itemCollectionFilter;
    public bool isItemOrderLocked;  // TODO FRONTEND AND EDITOR: make sure this is set when the item is part of a matrix
    public TItem[] items = new TItem[0];
    public bool[] doesItemPassFilter = new bool[0];     // Do not serialized, call apply filter when loading instead.

    public TItem this [int index]
    {
        get { return items[index]; }
    }

    public int Length
    {
        get { return items.Length; }
    }

    public static TItemCollection Create(string name, EnumSetting rarities, EnumSetting books)
    {
        TItemCollection newItemCollection = CreateInstance<TItemCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Collection name invalid, name cannot start with Default");

        newItemCollection.name = name;
        newItemCollection.rarities = rarities;
        newItemCollection.books = books;
        newItemCollection.itemCollectionFilter = ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>.CreateBlank(rarities, books);
        newItemCollection.items = new TItem[0];

        SaveableHolder.AddSaveable(newItemCollection);

        return newItemCollection;
    }

    public TItemCollection CreateCopyFromFilter ()
    {
        itemCollectionFilter.ApplyFilter(this as TItemCollection);
        TItemCollection newItemCollection = CreateInstance<TItemCollection>();
        newItemCollection.name = "CopyOf" + name;
        newItemCollection.rarities = rarities;
        newItemCollection.books = books;
        newItemCollection.itemCollectionFilter = itemCollectionFilter.Duplicate();

        List<TItem> filteredItems = new List<TItem>();
        for(int i = 0; i < items.Length; i++)
        {
            if (doesItemPassFilter[i])
                filteredItems.Add(items[i]);
        }
        newItemCollection.items = filteredItems.ToArray();
        newItemCollection.doesItemPassFilter = new bool[newItemCollection.items.Length];

        SaveableHolder.AddSaveable(newItemCollection);

        return newItemCollection;
    }

    public void ApplyFilter ()
    {
        itemCollectionFilter.ApplyFilter(this as TItemCollection);
    }

    public bool AddBlankItem ()
    {
        return AddItem(Item<TItem>.CreateBlank(rarities, books));
    }

    public bool AddItem (TItem newItem)
    {
        if (!NameIsUnique (newItem.name))
            return false;

        TItem[] newItems = new TItem[items.Length + 1];
        for (int i = 0; i < items.Length; i++)
        {
            newItems[i] = items[i];
        }
        newItems[items.Length] = newItem;

        return true;
    }

    public bool RemoveItemAt(int index)
    {
        if (index >= items.Length || index < 0)
            return false;

        TItem[] newItems = new TItem[items.Length - 1];
        for (int i = 0; i < newItems.Length; i++)
        {
            int oldItemIndex = i < index ? i : i + 1;
            newItems[i] = items[oldItemIndex];
        }

        return true;
    }

    public bool RemoveItem (TItem itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                return RemoveItemAt(i);
            }
        }

        return false;
    }

    public bool NameIsUnique (string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == itemName)
                return false;
        }
        return true;
    }

    public void SortByNameAssending ()
    {
        if(!isItemOrderLocked)
            items = items.OrderBy(item => item.name).ToArray();
    }

    public void SortByNameDecending ()
    {
        if (!isItemOrderLocked)
            items = items.OrderByDescending(item => item.name).ToArray();
    }

    public void SortByCostAssending()
    {
        if (!isItemOrderLocked)
            items = items.OrderBy(item => item.cost).ToArray();
    }

    public void SortByCostDecending()
    {
        if (!isItemOrderLocked)
            items = items.OrderByDescending(item => item.cost).ToArray();
    }

    public void SortByRarityAssending()
    {
        if (!isItemOrderLocked)
            items = items.OrderBy(item => item.rarity.GetIndex()).ToArray();
    }

    public void SortByRarityDecending()
    {
        if (!isItemOrderLocked)
            items = items.OrderByDescending(item => item.rarity.GetIndex()).ToArray();
    }

    public void SortByBookAssending()
    {
        if (!isItemOrderLocked)
            items = items.OrderBy(item => item.book.GetIndex()).ToArray();
    }

    public void SortByBookDecending()
    {
        if (!isItemOrderLocked)
            items = items.OrderByDescending(item => item.book.GetIndex()).ToArray();
    }

    public void SortByPageAssending()
    {
        if (!isItemOrderLocked)
            items = items.OrderBy(item => item.page).ToArray();
    }

    public void SortByPageDecending()
    {
        if (!isItemOrderLocked)
            items = items.OrderByDescending(item => item.page).ToArray();
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += rarities.name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];
        jsonString += ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>.GetJsonString(itemCollectionFilter) + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(isItemOrderLocked) + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Item<TItem>.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        rarities = EnumSetting.Load(splitJsonString[1]);
        books = EnumSetting.Load(splitJsonString[2]);
        itemCollectionFilter = ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>.CreateFromJsonString(splitJsonString[3]);
        isItemOrderLocked = Wrapper<bool>.CreateFromJsonString(splitJsonString[4]);

        items = new TItem[splitJsonString.Length - 5];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Item<TItem>.CreateFromJsonString(splitJsonString[i + 5]);
        }
        
        itemCollectionFilter.ApplyFilter(this as TItemCollection);
    }
}
