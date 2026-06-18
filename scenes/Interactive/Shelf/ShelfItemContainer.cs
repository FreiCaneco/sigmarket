using Godot;
using sigmarket.Scenes.Item;
using ItemData = sigmarket.Scenes.Item.ItemData;

namespace sigmarket.Scenes.Interactive.Shelf;

public partial class ShelfItemContainer : Node, IItemContainer
{
    [Signal] public delegate void ShelfItemContainerChangedEventHandler();
    
    private ItemData[] Slots { get;  set; }

    public void Configure(ShelfData shelfData)
    {
        Slots = new ItemData[shelfData.Capacity];
    }

    public ItemData GetItem(int index)
    {
        return Slots[index];
    }

    public void SetItem(int index, ItemData item)
    {
        Slots[index] = item;
        EmitSignal(SignalName.ShelfItemContainerChanged);
    }
    
    public void SwapItems(int fromIndex, int toIndex)
    {
        (Slots[fromIndex], Slots[toIndex]) = (Slots[toIndex], Slots[fromIndex]);
        EmitSignal(SignalName.ShelfItemContainerChanged);
    }
}
