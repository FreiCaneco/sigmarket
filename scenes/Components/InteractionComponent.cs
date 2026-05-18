using Godot;
using sigmarket.Shared.Singletons;

namespace sigmarket.Scenes.Components;

[Tool]
public partial class InteractionComponent : Node2D
{
    
    [Signal] public delegate void PlayerInteractedEventHandler();
    
    [Export] public Shape2D Shape;
    private CollisionShape2D _collisionShape;
    private Area2D _area2D;
    private TextureRect _interactionSprite;
    private float _interactionSpriteOriginalY;
    
    private Tween _hoverTween;
    private Tween _opacityTween;
    
    private bool _interacting;
    private bool _isPlayerInArea;

    public override void _Ready()
    {
        _area2D = GetNode<Area2D>("Area2D");
        _collisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        _interactionSprite = GetNode<TextureRect>("TextureRect");
        _interactionSpriteOriginalY = _interactionSprite.Position.Y;

        if (Shape != null)
        {
            _collisionShape.Shape = Shape;
        }

        _area2D.BodyEntered += OnArea2DBodyEntered;
        _area2D.BodyExited += OnArea2DBodyExited;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_isPlayerInArea && @event.IsActionPressed("interact"))
        {
            EmitSignal(SignalName.PlayerInteracted);
            StartInteractionAnimation();
            _interacting = true;
        }

        if (@event.IsActionPressed("exit") && _interacting)
        {
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractionStopped);
            _interacting = false;
        }
        
    }

    private void OnArea2DBodyEntered(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            ResetTweens();
            StartHoverAnimation();
            _isPlayerInArea = true;
        }
    }
    private void OnArea2DBodyExited(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            ResetTweens();
            ClearInteractionSpriteOpacity();
            _isPlayerInArea = false;
            _interacting = false;
        }
    }

    // Animations 
    private void StartHoverAnimation()
    {
        _opacityTween = CreateTween();
        _opacityTween.TweenProperty(_interactionSprite, "modulate:a", 1, 0.1);
        
        _hoverTween = CreateTween();
        _hoverTween.SetLoops();
        _hoverTween.SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        _hoverTween.TweenProperty(_interactionSprite, "position:y", _interactionSpriteOriginalY - 2, 0.3);
        _hoverTween.TweenProperty(_interactionSprite, "position:y", _interactionSpriteOriginalY, 0.3);
    }

    private void StartInteractionAnimation()
    {
        ResetTweens();
        ClearInteractionSpriteOpacity();
    }

    // Utilities
    private void ClearInteractionSpriteOpacity()
    {
        _opacityTween = CreateTween();
        _opacityTween.TweenProperty(_interactionSprite, "modulate:a", 0, 0.1);
    }
    
    private void ResetTweens()
    {
        if (_hoverTween != null && _hoverTween.IsValid()  ) _hoverTween.Kill();
        if( _opacityTween != null && _opacityTween.IsValid())_opacityTween.Kill();
    }
}