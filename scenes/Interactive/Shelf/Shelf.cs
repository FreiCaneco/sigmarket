using Godot;
using sigmarket.Scenes.Components;
using sigmarket.Shared.Singletons;
using sigmarket.Systems.Camera;

namespace sigmarket.Scenes.Interactive.Shelf;

public partial class Shelf : Node2D
{
    // Nodes e Componentes.
    [Export] public ShelfData ShelfInfo;
    private Sprite2D _sprite;
    private Marker2D _cameraPosition;
    
    private InteractionComponent _interactionComponent;
    private ShelfItemContainer _itemContainer;
    private ShelfSlots _shelfSlots;
    
    // Dependencias dos componentes
    private CanvasLayer _uiCanvas;
    private Camera _camera;
    
    // Sprite
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
    
    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _cameraPosition = GetNode<Marker2D>("CameraPosition");
        _interactionComponent = GetNode<InteractionComponent>("InteractionComponent");
        
        if (_atlasTexture != null) _sprite.Texture = _atlasTexture;
        
        // Dependencias
        _camera = (Camera)GetTree().GetFirstNodeInGroup("Camera");
        _uiCanvas = (CanvasLayer)GetTree().GetFirstNodeInGroup("Ui");
        
        // Componentes
        _itemContainer = GetNode<ShelfItemContainer>("ShelfItemContainer");
        _itemContainer.Configure(ShelfInfo);
        
        _shelfSlots = GetNode<ShelfSlots>("ShelfSlots");
        _shelfSlots.Configure(ShelfInfo,_itemContainer,_camera,_uiCanvas);


        // Sinais
        _interactionComponent.PlayerInteracted += OnPlayerInteraction;
        
    }
    
    private void OnPlayerInteraction()
    {
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.ShelfInteracted, this);
    }
}
