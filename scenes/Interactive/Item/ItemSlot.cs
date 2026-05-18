using Godot;

namespace sigmarket.Scenes.Interactive.Item;

public partial class ItemSlot : TextureRect
{
    [Export] public ItemResource ItemResource { get; set; }

    public override void _Ready()
    {
        if (ItemResource != null)
        {
            Texture = ItemResource.ItemTexture;
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (ItemResource == null)
        {
            return new Variant();
        }
        
        var data = new Godot.Collections.Dictionary 
        {
            {"ItemResource", ItemResource},
            {"originNode", this},
        };
        
        TextureRect previewTexture = new();
        previewTexture.Texture = ItemResource.ItemTexture;

        Control preview = new();
        preview.AddChild(previewTexture);
        Modulate = new Color(Modulate, 0.0f);
        
        SetDragPreview(preview);
        return data;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        bool canDrop = data.AsGodotDictionary().ContainsKey("ItemResource");
        return canDrop;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var dict = data.AsGodotDictionary();
        ItemSlot originNode = (ItemSlot)dict["originNode"];
        var draggedItem = dict["ItemResource"];

        var currentItemHere = ItemResource;

        ItemResource = (ItemResource)draggedItem;
        Texture = ItemResource.ItemTexture;
        Modulate = new Color(Modulate);
        
        originNode.ItemResource = currentItemHere;

        originNode.Texture = currentItemHere?.ItemTexture;

        originNode.Modulate = new Color(originNode.Modulate);
    }

    public override void _Notification(int what)
    {
        if (what == NotificationDragEnd && !IsDragSuccessful())
        {
            Modulate = new Color(Modulate);
        }
    }
}