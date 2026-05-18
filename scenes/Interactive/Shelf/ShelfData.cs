using Godot;

namespace sigmarket.Scenes.Interactive.Shelf;

[GlobalClass]
public partial class ShelfData : Resource
{
    public enum ShelfTypes
    {
        Square2X2,
        Square3X3,
    }

    [Export] public ShelfTypes ShelfType { get; set; }

    public ShelfData() : this(ShelfTypes.Square2X2){}
    public ShelfData(ShelfTypes shelfType)
    {
        ShelfType = shelfType;
    }

}