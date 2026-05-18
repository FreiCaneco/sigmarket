using Godot;
using sigmarket.Scenes.Components;
using sigmarket.Shared.Singletons;
using sigmarket.Systems.Camera;

namespace sigmarket.Scenes.Interactive.Shelf;

public partial class Shelf : Node2D
{
    private Sprite2D _sprite;
    private Marker2D _cameraPosition;
    private InteractionComponent _interactionComponent;
     
    [Export] public ShelfData ShelfInfo;

    private AtlasTexture _atlasTexture;
    [Export] public AtlasTexture ShelfTexture
    {
        get => _atlasTexture;
        set
        {
            _atlasTexture = value;
            if (_sprite != null)
            {
                _sprite.Texture = _atlasTexture;
            }
        }
    }

    private CanvasLayer _uiCanvas;
    private GridContainer _slotsGrid;
    private Vector2 _slotsGridOriginalPosition;
    
    private Camera  _camera;
    [Export] public PackedScene ItemScene; // GD.Load<PackedScene>("res://Scenes/Interactive/Item/item.tscn");
    
    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _cameraPosition = GetNode<Marker2D>("CameraPosition");
        _interactionComponent = GetNode<InteractionComponent>("InteractionComponent");
        _camera = (Camera)GetTree().GetFirstNodeInGroup("Camera");
        _uiCanvas = (CanvasLayer)GetTree().GetFirstNodeInGroup("Ui");

        if (_atlasTexture != null)
        {
            _sprite.Texture = _atlasTexture;
        }

        _camera.CameraAnimationEnded += OnCameraFinishedAnimation;
        _interactionComponent.PlayerInteracted += OnPlayerInteraction;
        
        AddSlotsBasedOnType();
    }

    public override void _Process(double delta)
    {
        if (_slotsGrid != null && _slotsGrid.Visible && _slotsGrid.GetParent() == _uiCanvas)
        {
            var shelfScreenPosition = GetGlobalTransformWithCanvas().Origin;
            var z = _camera.Zoom.X;
            
            var gridOffset = new Vector2(-9,-12);
            _slotsGrid.GlobalPosition = (shelfScreenPosition + (gridOffset * z));
            _slotsGrid.Scale = new Vector2(z, z);
        }
    }
    
    private void AddSlotsBasedOnType()
    {
        _slotsGrid = new GridContainer();
        AddChild(_slotsGrid);
        
        int numOfSlots;
        _slotsGrid.AddThemeConstantOverride("h_separation",2);
        _slotsGrid.AddThemeConstantOverride("v_separation",2);
        _slotsGrid.MouseFilter = Control.MouseFilterEnum.Pass;

        switch (ShelfInfo.ShelfType)
        {
            case (ShelfData.ShelfTypes.Square2X2):
                _slotsGrid.Columns = 2;
                _slotsGrid.Size = new Vector2(18f, 18f);
                _slotsGrid.Position = new Vector2(-9, -12);
                _slotsGridOriginalPosition = _slotsGrid.Position;
                numOfSlots = 4;
                for (int i = 0; i < numOfSlots; i++)
                {
                    var newItem = ItemScene.Instantiate();
                    _slotsGrid.AddChild(newItem);
                }
                break;
        }
    }

    private void OnPlayerInteraction()
    {
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.ShelfInteracted, this);
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