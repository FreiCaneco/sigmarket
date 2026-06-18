using Godot;
using ItemData = sigmarket.Scenes.Item.ItemData;

namespace sigmarket.Systems.Inventory;

[GlobalClass]
public partial class ItemCollection : Resource
{
    [Export] public Godot.Collections.Array<ItemData> Items { get; set; }
}