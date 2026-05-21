using Godot;
using sigmarket.Scenes.Interactive.Item;

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
    [Export] public int Capacity { get; set; }
    
    public ShelfData() : this(ShelfTypes.Square2X2, 4){}
    public ShelfData(ShelfTypes shelfType,  int capacity)
    {
        ShelfType = shelfType;
        Capacity = capacity;
    }

}