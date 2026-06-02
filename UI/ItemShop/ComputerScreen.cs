using System.Collections.Generic;
using Godot;

namespace sigmarket.Ui.ItemShop;

public partial class ComputerScreen : Control
{
    [Export] public VBoxContainer AreasButtons;
    [Export] public VBoxContainer ItemsButtons;
    [Export] public VBoxContainer UpgradesButtons;
    
    [Export] public TextureButton AreasTab;
    [Export] public TextureButton ItemsTab;
    [Export] public TextureButton UpgradesTab;
    [Export] public TextureButton SecretShopTab;
    
 
    public override void _Ready()
    {
        AreasTab.Pressed += () => ShowSection(AreasButtons);
        ItemsTab.Pressed += () => ShowSection(ItemsButtons);
        UpgradesTab.Pressed += () => ShowSection(UpgradesButtons);
    }
    
    private void ShowSection(Control sectionToShow)
    {
        AreasButtons.Hide();
        ItemsButtons.Hide();
        UpgradesButtons.Hide();

        sectionToShow.Show();
    }

}