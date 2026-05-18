using Godot;

namespace sigmarket.Shared.Singletons;

public partial class SignalBus : Node
{
    public static SignalBus Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
    // Used for setting camera position and boundaries.
    [Signal] public delegate void LastPixelFromScreenChangedEventHandler();
    [Signal] public delegate void ComputerInteractedEventHandler();
    
    // Signal of the objects that have the interaction component
    [Signal] public delegate void PlayerInteractionStoppedEventHandler();
    [Signal] public delegate void DoorInteractedEventHandler();
    [Signal] public delegate void ShelfInteractedEventHandler(Node2D shelf);
    
    
}