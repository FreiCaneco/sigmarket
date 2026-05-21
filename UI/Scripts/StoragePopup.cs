using Godot;
using sigmarket.Scenes.Interactive.Item;
using sigmarket.Shared.Singletons;
using sigmarket.Systems.Inventory;

namespace sigmarket.Ui.Scripts;

public partial class StoragePopup : Control
{
    [Export] public Inventory Storage {get; set;}
    private GridContainer _gridContainer;
    private PackedScene _itemScene = GD.Load<PackedScene>("res://Scenes/Interactive/Item/item.tscn");
    
    public override void _Ready()
    {
        _gridContainer = GetNode<GridContainer>("Storage");
        LoadItemSlots();
        Visible = false;
        SignalBus.Instance.ShelfInteracted += OpenStorage;
        SignalBus.Instance.PlayerInteractionStopped += CloseStorage;
    }

    private void OpenStorage(Node2D shelf)
    {
        Visible = true;
    }
    
    private void CloseStorage()
    {
        Visible = false;
    }
    
    private void LoadItemSlots()
    {
        foreach (var itemRes in Storage.Items)
        {
            ItemSlot itemSlot = (ItemSlot)_itemScene.Instantiate();
            itemSlot.ItemData = itemRes;
            _gridContainer.AddChild(itemSlot);
        }
    }
}