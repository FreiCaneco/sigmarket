using Godot;
using sigmarket.Shared.Singletons;
using sigmarket.Ui.Scripts;

namespace sigmarket;

public partial class Main : Node2D
{
	private SubViewport _subViewport;
	private CanvasLayer _canvasLayer;
	
	private PackedScene _gameScene = GD.Load<PackedScene>("res://Scenes/game.tscn");
	private PackedScene _computerMenuScene = GD.Load<PackedScene>("res://UI/ItemShop/computer_screen.tscn");

	[Export] public MainMenu MainMenu;

	public override void _Ready()
	{
		_subViewport = GetNode<SubViewport>("160x90Viewport/SubViewport");
		_canvasLayer = GetNode<CanvasLayer>("Ui");
		
		SignalBus.Instance.ComputerInteracted += OnComputerInteracted;
		MainMenu.PlayPressed += OnMainMenuPlayPressed;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("exit") && _canvasLayer.Visible) ExitComputer();
	}

	private void OnMainMenuPlayPressed()
	{
		var gameScene = _gameScene.Instantiate();
		var computerMenuScene = (Control)_computerMenuScene.Instantiate();

		_subViewport.GetChild(0).QueueFree();
		_subViewport.AddChild(gameScene);
		
		_canvasLayer.AddChild(computerMenuScene);
		computerMenuScene.Visible = false;
	}

	private void OnComputerInteracted()
	{
		GetTree().Paused = true;
		_canvasLayer.Show();
	}

	private void ExitComputer()
	{
		GetTree().Paused = false;
		_canvasLayer.Hide();
	}

	public override void _ExitTree()
	{
		SignalBus.Instance.ComputerInteracted -= OnComputerInteracted;
	}
}
