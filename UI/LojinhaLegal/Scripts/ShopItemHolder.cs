using Godot;
using sigmarket.Scenes.Item;
using sigmarket.Systems.UiController;

namespace sigmarket.Ui.LojinhaLegal.Scripts;

public partial class ShopItemHolder : PanelContainer
{
    [Export] public Button PopupOpener;
    [Export] public ItemData ItemData { get; set; }
    [Export] public TextureRect ItemSprite;
    private PopupsHolder _popupsHolder;

    public override void _Ready()
    {
        ItemSprite = GetNode<TextureRect>("Button/MarginContainer/ItemSprite");
        if (ItemData != null)
        {
            ItemSprite.Texture = ItemData.ItemTexture;
        }

        _popupsHolder = GetTree().GetFirstNodeInGroup("PopupsHolder") as PopupsHolder;
        PopupOpener.Pressed += () => _popupsHolder.CreatePopup(ItemData);
    }

    public void Configure(ItemData itemData)
    {
        ItemSprite.Texture = null;
        ItemData = null;
        if (itemData != null)
        {
            ItemData = itemData;
            ItemSprite.CustomMinimumSize = ItemData.ItemTexture.Region.Size * SetItemSizeMultiplier(ItemData.ItemTag);
            ItemSprite.Texture = ItemData.ItemTexture;
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
}