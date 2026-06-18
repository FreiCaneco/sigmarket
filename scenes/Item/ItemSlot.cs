using Godot;
using Godot.Collections;

namespace sigmarket.Scenes.Item;

public partial class ItemSlot : TextureRect
{
    private int _index;
    private Node _itemContainer;
    
    private const string ContainerOriginKey = "itemContainerOrigin";
    private const string IndexOriginKey = "indexOrigin";
    
    public void Configure(Node itemContainer, int index)
    {
        _index = index;
        _itemContainer = itemContainer;
        Refresh();
    }
    public int GetIndex() => _index;

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var data = new Dictionary<Variant,Variant>
        {
            {ContainerOriginKey, _itemContainer},
            {IndexOriginKey,_index},
        };
        
        TextureRect previewTexture = new();
        if (_itemContainer is IItemContainer itemContainer)
        {
            var item = itemContainer.GetItem(_index);
            if  (item == null) return new Variant();
            previewTexture.Texture = item.ItemTexture;
        }
        
        Control preview = new();
        preview.AddChild(previewTexture);
        Modulate = new Color(Modulate, 0.0f);
        
        SetDragPreview(preview);
        return data;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        var dict = data.AsGodotDictionary();
        return dict.ContainsKey(ContainerOriginKey) && dict.ContainsKey(IndexOriginKey) && dict[ContainerOriginKey].AsGodotObject() is Node; 
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var dict = data.AsGodotDictionary();
        
        var originContainer = dict[ContainerOriginKey].AsGodotObject() as Node;
        int originIndex = (int)dict[IndexOriginKey];


        if (originContainer is not IItemContainer oldContainer) return;
        if (_itemContainer is not IItemContainer targetContainer) return;
        
        if (oldContainer == targetContainer)
        {
            targetContainer.SwapItems(originIndex, _index);
            return;
        }
        
        var originItem = oldContainer.GetItem(originIndex);
        var targetItem = targetContainer.GetItem(_index);
        
        oldContainer.SetItem(originIndex, targetItem);
        targetContainer.SetItem(_index, originItem);
    }
    
    public void Refresh()
    {
        if (_itemContainer is not IItemContainer itemContainer)
            return;

        var itemData = itemContainer.GetItem(_index);
        Texture = itemData?.ItemTexture;
    }
    
    public override void _Notification(int what)
    {
        if (what == NotificationDragEnd)
        {
            Modulate = new Color(Modulate, 1.0f);
        }
    }
}
