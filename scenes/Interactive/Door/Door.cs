using Godot;
using sigmarket.Scenes.Components;
using sigmarket.Shared.Singletons;

namespace sigmarket.Scenes.Interactive.Door;

public partial class Door : Node2D
{
    private InteractionComponent _interactionComponent;
    
    public override void _Ready()
    {
        _interactionComponent = GetNode<InteractionComponent>("InteractionComponent");
        _interactionComponent.PlayerInteracted += OnPlayerComponentInteraction;
    }

    private void OnPlayerComponentInteraction()
    {
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.DoorInteracted);
    }
}