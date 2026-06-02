using Godot;
using sigmarket.Scenes.Interactive.Item;
using sigmarket.Systems.Inventory;

namespace sigmarket.Ui.PlayerInventoryPopup;

public partial class PlayerItemContainer : Node, IItemContainer
{
    [Signal] public delegate void PlayerItemContainerChangedEventHandler();
    private ItemData[] _slots;

    public void Configure(Inventory playerInventory)
    {
        _slots = new ItemData[playerInventory.Items.Count];

        for (int i = 0; i < playerInventory.Items.Count; i++)
        {
            _slots[i] = playerInventory.Items[i];
        }
    }

    public ItemData GetItem(int index)
    {
        return _slots[index];
    }

    public void SetItem(int index, ItemData item)
    {
        _slots[index] = item;
        EmitSignal(SignalName.PlayerItemContainerChanged);
    }

    public void SwapItems(int fromIndex, int toIndex)
    {
        (_slots[fromIndex],_slots[toIndex]) = (_slots[toIndex],_slots[fromIndex]);
        EmitSignal(SignalName.PlayerItemContainerChanged);
    }
}
