using Godot;
using sigmarket.Scenes.Components;
using sigmarket.Shared.Singletons;

namespace sigmarket.Scenes.Interactive.CashRegister;

public partial class CashRegister : Node2D
{
    private InteractionComponent _interactionComponent;
    
    private Godot.Collections.Array<Node2D> _sections = new();
    PackedScene _newSectionScene = GD.Load<PackedScene>("res://Scenes/Sections/yellow_checkered.tscn");
    public override void _Ready()
    {
        _interactionComponent = GetNode<InteractionComponent>("InteractionComponent");
        _sections.Add((Node2D)GetTree().GetFirstNodeInGroup("Sections"));
        
        _interactionComponent.PlayerInteracted += OnPlayerComponentInteraction;
    }
    
    private void AddSection()
    {
        // Alterar isso, pois futuramente o jogador que vai escolher qual vai ser o tipo de section a ser adicionada
        Node2D newSection = (Node2D) _newSectionScene.Instantiate();
        newSection.Position = GlobalData.Instance.FinalPixelPosition;
        GlobalData.Instance.FinalPixelPosition += new Vector2(160, 0);
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.LastPixelFromScreenChanged);
        _sections.Add(newSection);
    }

    private void OnPlayerComponentInteraction()
    {
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.ComputerInteracted);
    }
}