using Godot;
using sigmarket.Systems.Inventory;

namespace sigmarket.Ui.LojinhaLegal.Scripts;

public partial class SidebarButton : TextureButton
{
    [Signal] public delegate void SidebarButtonPressedEventHandler(ItemCollection itemsToShow);
    [Export] public ItemCollection ItemsToShow;
}