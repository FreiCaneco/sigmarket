using System.Collections.Generic;
using System.Linq;
using sigmarket.Systems.Inventory;
using Godot;

namespace sigmarket.Ui.LojinhaLegal.Scripts;

public partial class LojinhaLegal : Control
{
    [Export] public TextureButton AreasTab;
    [Export] public VBoxContainer AreasButtons;
    
    [Export] public TextureButton ItemsTab;
    [Export] public VBoxContainer ItemsButtons;
    
    [Export] public TextureButton UpgradesTab;
    [Export] public VBoxContainer UpgradesButtons;
    
    [Export] public TextureButton CartTab;
    [Export] public HBoxContainer CartPage;
    [Export] public MarginContainer ItemHolderPage;
    [Export] public GridContainer AllItemHolders;
    
    private IEnumerable<Control> AllSideButtons => new List<VBoxContainer> { AreasButtons, ItemsButtons, UpgradesButtons };
    private List<SidebarButton> _sidebarButtons = new();
    private IEnumerable<Control> Pages => new List<Control> { ItemHolderPage, CartPage };
    private List<ShopItemHolder> _itemHolders= new();
    
    private ItemCollection _cartItems = new();
    
    public override void _Ready()
    {
        AreasTab.Pressed += () => ChangeSideButtons(AreasButtons);
        AreasTab.Pressed += () => ChangeMainContent(ItemHolderPage);
        ItemsTab.Pressed += () => ChangeSideButtons(ItemsButtons);
        ItemsTab.Pressed += () => ChangeMainContent(ItemHolderPage);
        UpgradesTab.Pressed += () => ChangeSideButtons(UpgradesButtons);
        UpgradesTab.Pressed += () => ChangeMainContent(ItemHolderPage);
        CartTab.Pressed += () => ChangeMainContent(CartPage);
        
        
        GetAllSidebarButtons();
        foreach (SidebarButton sidebarButton in _sidebarButtons)
        {
            sidebarButton.Pressed += () => ChangeGrid(sidebarButton.ItemsToShow);
        }
        
        _itemHolders.AddRange(AllItemHolders.GetChildren().OfType<ShopItemHolder>());
    }
    
    private void ChangeSideButtons(Control sectionToShow)
    {
        foreach (var sideButton in AllSideButtons) sideButton.Hide();
        sectionToShow.Show();
    }

    private void ChangeMainContent(Control pageToShow)
    {
        foreach(var page in Pages) page.Hide();
        pageToShow.Show();
    }

    private void GetAllSidebarButtons()
    {
        _sidebarButtons.AddRange(
            AllSideButtons
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
            _itemHolders[i].Show();
            _itemHolders[i].Configure(itemsToShow.Items[i]);
        }
        
        for (int i = itemsToShow.Items.Count; i < _itemHolders.Count; i++)
        {
            _itemHolders[i].Hide();
        }
    }
    
}