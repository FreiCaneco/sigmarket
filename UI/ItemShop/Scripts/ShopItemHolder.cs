using Godot;
using sigmarket.Scenes.Item;

namespace sigmarket.Ui.ItemShop.Scripts;

public partial class ShopItemHolder : PanelContainer
{
    [Export] public Button PopupOpener;
    [Export] public ItemData ItemData { get; set; }
    private TextureRect _itemSprite;

    private CanvasLayer _uiCanvas;

    public override void _Ready()
    {
        _itemSprite = GetNode<TextureRect>("Button/MarginContainer/ItemSprite");
        if (ItemData != null)
        {
            _itemSprite.Texture = ItemData.ItemTexture;
        }

        PopupOpener.Pressed += () => OpenPopup(ItemData);
        _uiCanvas = GetTree().GetFirstNodeInGroup("UiCanvas") as CanvasLayer;
    }

    public void Configure(ItemData itemData)
    {
        _itemSprite.Texture = null;
        if (itemData != null)
        {
            ItemData = itemData;
            _itemSprite.CustomMinimumSize = ItemData.ItemTexture.Region.Size * SetItemSizeMultiplier(ItemData.ItemTag);
            _itemSprite.Texture = ItemData.ItemTexture;
        }
    }
    
    private int SetItemSizeMultiplier(ItemData.Tag itemTag)
    {
        // Esse valor de 6, deve ser uma variável global, que se altera baseado na resolução.
        // Nesse caso, estamos alterando objetos de uma resolução 160x90.
        int currentResolutionMultiplier = 6;
        switch (itemTag)
        {
            case ItemData.Tag.Fruit:
                return currentResolutionMultiplier * 2;
        }
        return currentResolutionMultiplier;
    }

    private void OpenPopup(ItemData itemData)
    {
        if (itemData == null)
        {
            GD.Print("Item Data is null");
            return;
        }
        var itemPopup = GD.Load<PackedScene>("res://Ui/ItemPopup/item_popup_layout.tscn");
        if (itemPopup.Instantiate() is not ItemPopup.ItemPopup popup) return;
        popup.ItemData = itemData;
        _uiCanvas.AddChild(popup);
    }

    
}