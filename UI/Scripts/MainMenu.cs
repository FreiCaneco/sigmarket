using Godot;

namespace sigmarket.Ui.Scripts;

public partial class MainMenu : Control
{
    [Signal] public delegate void PlayPressedEventHandler();
    
    private Button _playButton;
    public override void _Ready()
    {
        _playButton = GetNode<Button>("VBoxContainer/PlayButton");
        _playButton.Pressed += OnPlayPressed;
    }
    
    private void OnPlayPressed()
    {
        EmitSignal(SignalName.PlayPressed);
    }
}