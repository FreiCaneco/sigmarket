using Godot;
using sigmarket.Shared.Singletons;

namespace sigmarket.Scenes.Player;

public partial class PlayerVfx : Node
{
    private CharacterBody2D _player;
    private AnimationPlayer _anim;

    public override void _Ready()
    {
        _player = GetParent<CharacterBody2D>();
        _anim = _player.GetNode<AnimationPlayer>("AnimationPlayer");

        SignalBus.Instance.ShelfInteracted += ShelfInteraction;
        SignalBus.Instance.PlayerInteractionStopped += ShelfExit;
    }

    private void ShelfInteraction(Node2D shelf)
    {
        _player.Velocity = Vector2.Zero;
        _player.SetPhysicsProcess(false);
        Tween tween = CreateTween();
        tween.TweenProperty(_player, "modulate:a", 0.2, 0.5f);
    }

    private async void ShelfExit()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(_player, "modulate:a", 1, 0.5);
        await ToSignal(tween, Tween.SignalName.Finished);
        _player.SetPhysicsProcess(true);
    }
}