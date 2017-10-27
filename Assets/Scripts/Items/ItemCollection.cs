using UnityEngine;

public abstract class ItemCollection<TChild, TItem> : Saveable<TChild>
    where TChild : ItemCollection<TChild, TItem>
    where TItem : Item<TItem>
{
    public TItem[] items = new TItem[0];

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
}
