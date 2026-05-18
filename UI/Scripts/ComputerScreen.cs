using Godot;

namespace sigmarket.Ui.Scripts;

public partial class ComputerScreen : Control
{
    private VBoxContainer _areasSidebar;
    private VBoxContainer _itemsSidebar;
    private VBoxContainer _upgradesSidebar;
    private Godot.Collections.Array<VBoxContainer> _sidebarContainers = new();
    
    private ButtonGroup _sidebarButtons = new();
    
    [Export] public TabContainer TabsContainer;

    public override void _Ready()
    {
        TabsContainer.TabChanged += OnTabContainerTabChanged;
        
        foreach (var container in _sidebarContainers)
        {
            foreach (var button in container.GetChildren())
            {
                if (button is TextureButton buttonContainer)
                {
                    buttonContainer.ButtonGroup = _sidebarButtons;
                }
            }
        }
    }

    private void OnTabContainerTabChanged(long tab)
    {
        for (int i = 0; i < _sidebarContainers.Count; i++)
        {
            _sidebarContainers[i].Visible = (tab == i);
        }
    }
}