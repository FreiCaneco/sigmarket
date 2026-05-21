using Godot;
using sigmarket.Scenes.Interactive.Item;

namespace sigmarket.Scenes.Interactive.Shelf;

public partial class ShelfStorage : Node, IItemStorage
{
    [Signal] public delegate void ShelfStorageChangedEventHandler();
    
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
        EmitSignal(SignalName.ShelfStorageChanged);
    }
    
    public void SwapItems(int fromIndex, int toIndex)
    {
        (Slots[fromIndex], Slots[toIndex]) = (Slots[toIndex], Slots[fromIndex]);
        EmitSignal(SignalName.ShelfStorageChanged);
    }
}
