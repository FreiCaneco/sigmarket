using Godot;
using sigmarket.Shared.Singletons;

namespace sigmarket.Systems.Camera;

public partial class Camera : Camera2D
{
    private Node2D _player;
    public enum CameraAnimationType
    {
        GoToShelf,
        GoToPlayer
    }
    
    [Signal] public delegate void CameraAnimationEndedEventHandler(int animationType);
    
    public override void _Ready()
    {
        _player = (Node2D)GetTree().GetFirstNodeInGroup("Player");

        GD.Print(SignalBus.Instance);
        SignalBus.Instance.ShelfInteracted += GoToShelf;
        SignalBus.Instance.PlayerInteractionStopped += GoToPlayer;
        SignalBus.Instance.LastPixelFromScreenChanged += OnLastPixelChanged;
    }
    
    private async void GoToShelf(Node2D shelf)
    {  
        Marker2D correctPosition = shelf.GetNode<Marker2D>("CameraPosition");
        Tween tween = CreateTween();
        tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
        tween.SetParallel();
        tween.TweenProperty(this,"global_position",correctPosition.GlobalPosition,1);
        tween.TweenProperty(this, "zoom", new Vector2(2, 2), 1);
        await ToSignal(tween, Tween.SignalName.Finished);
        EmitSignal(SignalName.CameraAnimationEnded, (int)CameraAnimationType.GoToShelf);
    }

    private async void GoToPlayer()
    {
        Tween tween = CreateTween();
        tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
        tween.SetParallel();
        tween.TweenProperty(this, "global_position", _player.GlobalPosition, 0.5);
        tween.TweenProperty(this, "zoom", new Vector2(1, 1), 0.5);
        await ToSignal(tween, Tween.SignalName.Finished);
        EmitSignal(SignalName.CameraAnimationEnded, (int)CameraAnimationType.GoToPlayer);
    }

    private void OnLastPixelChanged()
    {
        LimitRight = (int)GlobalData.Instance.FinalPixelPosition.X;
    }
}