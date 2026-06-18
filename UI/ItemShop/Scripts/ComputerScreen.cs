using System.Collections.Generic;
using System.Linq;
using Godot;
using sigmarket.Systems.Inventory;

namespace sigmarket.Ui.ItemShop.Scripts;

public partial class ComputerScreen : Control
{
    [Export] public VBoxContainer AreasButtons;
    [Export] public VBoxContainer ItemsButtons;
    [Export] public VBoxContainer UpgradesButtons;
    private IEnumerable<VBoxContainer> Sections => new List<VBoxContainer> { AreasButtons, ItemsButtons, UpgradesButtons };
    private List<SidebarButton> _sidebarButtons = new();
    
    [Export] public GridContainer ItemHolderGrid;
    private List<ShopItemHolder> _itemHolders= new();
    
    [Export] public TextureButton AreasTab;
    [Export] public TextureButton ItemsTab;
    [Export] public TextureButton UpgradesTab;
    [Export] public TextureButton SecretShopTab;
    
    public override void _Ready()
    {
        AreasTab.Pressed += () => ShowSection(AreasButtons);
        ItemsTab.Pressed += () => ShowSection(ItemsButtons);
        UpgradesTab.Pressed += () => ShowSection(UpgradesButtons);
        
        GetAllSidebarButtons();
        foreach (SidebarButton sidebarButton in _sidebarButtons)
        {
            sidebarButton.Pressed += () => ChangeGrid(sidebarButton.ItemsToShow);
        }
        
        _itemHolders.AddRange(ItemHolderGrid.GetChildren().OfType<ShopItemHolder>());
    }
    
    private void ShowSection(Control sectionToShow)
    {
        foreach (var section in Sections) section.Hide();
        sectionToShow.Show();
    }

    private void GetAllSidebarButtons()
    {
        _sidebarButtons.AddRange(
            Sections
                .SelectMany(section => section.GetChildren())
                .OfType<SidebarButton>());
    }

    private void ChangeGrid(ItemCollection itemsToShow)
    {
        foreach (var itemHolder in _itemHolders)
        {
            itemHolder.Configure(null);
        }

        for (int i = 0; i < itemsToShow.Items.Count; i++)
        {
            _itemHolders[i].Configure(itemsToShow.Items[i]);
        }
    }
}