using Godot;
using sigmarket.Scenes.Interactive.Item;
using sigmarket.Systems.Camera;

namespace sigmarket.Scenes.Interactive.Shelf;

public partial class ShelfSlots : Node2D
{
    private ShelfData _shelfData;
    private GridContainer _slotsGrid;
    private Vector2 _slotsGridOriginalPosition;
    private CanvasLayer _uiCanvas;
    private Camera _camera;
    [Export] public PackedScene ItemScene;
    
    private ShelfItemContainer _itemContainer;
    
    public void Configure(ShelfData shelfData, ShelfItemContainer itemContainer, Camera camera,  CanvasLayer uiCanvas)
    {
        _shelfData = shelfData;
        _itemContainer = itemContainer;
        _uiCanvas = uiCanvas;
        _camera = camera;
        _camera.CameraAnimationEnded += OnCameraFinishedAnimation;
        AddSlotsBasedOnType();
        _itemContainer.ShelfItemContainerChanged += RefreshItems;
    }

    public override void _Process(double delta)
    {
        if (_slotsGrid is { Visible: true } && _slotsGrid.GetParent() == _uiCanvas)
        {
            var shelfScreenPosition = GetGlobalTransformWithCanvas().Origin;
            var z = _camera.Zoom.X;

            var gridOffset = new Vector2(-9, -12);
            _slotsGrid.GlobalPosition = (shelfScreenPosition + (gridOffset * z));
            _slotsGrid.Scale = new Vector2(z, z);
        }
    }
    
    // Fazer um refactor em massa de função para lidar com qualquer tipo de dado, atualmente so fazemos com 1 tipo específico
    private void AddSlotsBasedOnType()
    {
        _slotsGrid = new GridContainer();
        AddChild(_slotsGrid);

        int numOfSlots = _shelfData.Capacity;
        _slotsGrid.AddThemeConstantOverride("h_separation", 2);
        _slotsGrid.AddThemeConstantOverride("v_separation", 2);
        _slotsGrid.MouseFilter = Control.MouseFilterEnum.Pass;

        switch (_shelfData.ShelfType)
        {
            case (ShelfData.ShelfTypes.Square2X2):
                _slotsGrid.Columns = 2;
                _slotsGrid.Size = new Vector2(18f, 18f);
                _slotsGrid.Position = new Vector2(-9, -12);
                _slotsGridOriginalPosition = _slotsGrid.Position;
                for (int i = 0; i < numOfSlots; i++)
                {
                    ItemSlot itemSlot = (ItemSlot)ItemScene.Instantiate();
                    itemSlot.Configure(_itemContainer, i);
                    _slotsGrid.AddChild(itemSlot);
                }
                break;
        }
    }
    
    private void RefreshItems()
    {
        foreach (var itemSlot in _slotsGrid.GetChildren())
        {
            if (itemSlot is ItemSlot slot)
            {
                slot.Refresh();
            }
        }
    }
    
    private void OnCameraFinishedAnimation(int animType)
    {
        if (animType == (int)Camera.CameraAnimationType.GoToShelf)
        {
            _slotsGrid.Reparent(_uiCanvas,false);
        }

        if (animType == (int)Camera.CameraAnimationType.GoToPlayer)
        {
            _slotsGrid.Reparent(this);
            _slotsGrid.Position = _slotsGridOriginalPosition;  
        }
    }
}
