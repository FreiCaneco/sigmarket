using Godot;
using sigmarket.Shared.Singletons;

namespace sigmarket.Systems.Surrounding;

public partial class RightBoundary : StaticBody2D
{
    public override void _Ready()
    {
        SignalBus.Instance.LastPixelFromScreenChanged += OnNewSectionCreated;
    }

    private void OnNewSectionCreated()
    {
        Position = GlobalData.Instance.FinalPixelPosition;
    }
}