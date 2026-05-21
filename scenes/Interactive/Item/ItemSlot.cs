using System;
using Godot;
using Godot.Collections;

namespace sigmarket.Scenes.Interactive.Item;

public partial class ItemSlot : TextureRect
{
    private int _index;
    private Node _storage;
    
    private const string StorageOriginKey = "storageOrigin";
    private const string IndexOriginKey = "indexOrigin";
    
    public void Configure(Node storage, int  index)
    {
        _index = index;
        _storage = storage;
        Refresh();
    }
    public int GetIndex() => _index;

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var data = new Dictionary<Variant,Variant>
        {
            {StorageOriginKey, _storage},
            {IndexOriginKey,_index},
        };
        
        TextureRect previewTexture = new();
        if (_storage is IItemStorage storage)
        {
            var item = storage.GetItem(_index);
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
        return dict.ContainsKey(StorageOriginKey) && dict.ContainsKey(IndexOriginKey) && dict[StorageOriginKey].AsGodotObject() is Node; 
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var dict = data.AsGodotDictionary();
        
        var originStorage =  dict[StorageOriginKey].AsGodotObject() as Node;
        int originIndex = (int)dict[IndexOriginKey];


        if (originStorage is not IItemStorage oldStorage) return;
        if (_storage is not IItemStorage targetStorage) return;
        
        if (oldStorage == targetStorage)
        {
            targetStorage.SwapItems(originIndex, _index);
            return;
        }
        
        var originItem = oldStorage.GetItem(originIndex);
        var targetItem =  targetStorage.GetItem(_index);
        
        oldStorage.SetItem(originIndex, targetItem);
        targetStorage.SetItem(_index, originItem);
    }
    
    public void Refresh()
    {
        if (_storage is not IItemStorage storage)
            return;

        var itemData = storage.GetItem(_index);
        Texture = itemData?.ItemTexture;
    }
    
    public override void _Notification(int what)
    {
        if (what == NotificationDragEnd && !IsDragSuccessful())
        {
            Modulate = new Color(Modulate);
        }
    }
}