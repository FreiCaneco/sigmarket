using Godot;
using sigmarket.Systems.Inventory;

namespace sigmarket.Ui.ItemShop.Scripts;

public partial class SidebarButton : TextureButton
{
    [Signal] public delegate void SidebarButtonPressedEventHandler(ItemCollection itemsToShow);
    [Export] public ItemCollection ItemsToShow;
}