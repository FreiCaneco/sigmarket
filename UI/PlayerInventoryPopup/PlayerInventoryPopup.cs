using Godot;
using sigmarket.Shared.Singletons;
using sigmarket.Systems.Inventory;
using ItemSlot = sigmarket.Scenes.Item.ItemSlot;

namespace sigmarket.Ui.PlayerInventoryPopup;

public partial class PlayerInventoryPopup : Control
{
    [Export] public ItemCollection PlayerItemCollection {get; set;}
    private GridContainer _gridSlots;
    private PackedScene _itemSlotScene = GD.Load<PackedScene>("res://Scenes/Item/item_slot.tscn");
    private PlayerItemContainer _itemContainer;
    
    public override void _Ready()
    {
        Visible = false;
        
        _gridSlots = GetNode<GridContainer>("SlotsGrid");
        _itemContainer = GetNode<PlayerItemContainer>("PlayerItemContainer");
        _itemContainer.Configure(PlayerItemCollection);
        _itemContainer.PlayerItemContainerChanged += RefreshItems;
        AddSlotsToGrid();
        
        SignalBus.Instance.ShelfInteracted += OpenStorage;
        SignalBus.Instance.PlayerInteractionStopped += CloseStorage;
    }
    
    private void OpenStorage(Node2D shelf) { Visible = true; }
    private void CloseStorage() { Visible = false; }
    private void AddSlotsToGrid()
    {
        for (var i = 0; i < PlayerItemCollection.Items.Count; i++)
        {
            var itemSlot = (ItemSlot)_itemSlotScene.Instantiate();
            itemSlot.Configure(_itemContainer,i);
            _gridSlots.AddChild(itemSlot);
        }
    }
    private void RefreshItems()
    {
        foreach (var itemSlot in _gridSlots.GetChildren())
        {
            if (itemSlot is ItemSlot slot)
            {
                slot.Refresh();
            }
        }
    }
}
