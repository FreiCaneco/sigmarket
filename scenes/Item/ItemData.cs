using Godot;

namespace sigmarket.Scenes.Item;

[GlobalClass]
public partial class ItemData : Resource
{

    public enum Tag
    {
        Generic,
        Fruit,
        Furniture,
    }
    [Export] public Tag ItemTag { get; set; }
    [Export] public string ItemName { get; set; }
    [Export] public AtlasTexture ItemTexture { get; set; }
    [Export] public string ItemDescription { get; set; }
    [Export] public float ItemPrice { get; set; }

    public ItemData() : this(null, null, null, 0,Tag.Generic) {}

    public ItemData(string itemName, AtlasTexture itemTexture, string itemDescription, float itemPrice,  Tag itemTag)
    {
        ItemName = itemName;
        ItemTexture = itemTexture;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
        ItemTag = itemTag;
    }

}