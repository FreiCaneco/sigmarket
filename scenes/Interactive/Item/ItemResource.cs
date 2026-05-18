using Godot;

namespace sigmarket.Scenes.Interactive.Item;

public partial class ItemResource : Resource
{
    [Export] public string ItemName { get; set; }
    [Export] public AtlasTexture ItemTexture { get; set; }
    [Export] public string ItemDescription { get; set; }
    [Export] public float ItemPrice { get; set; }
}