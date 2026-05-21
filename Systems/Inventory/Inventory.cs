using Godot;
using sigmarket.Scenes.Interactive.Item;

namespace sigmarket.Systems.Inventory;

[GlobalClass]
public partial class Inventory : Resource
{
    [Export] public Godot.Collections.Array<ItemData> Items { get; set; }
}