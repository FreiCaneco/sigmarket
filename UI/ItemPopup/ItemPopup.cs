
using Godot;
using sigmarket.Scenes.Item;

namespace sigmarket.Ui.ItemPopup;

public partial class ItemPopup : PanelContainer
{
   [Export] public HBoxContainer TopBar;
   [Export] public TextureButton CloseButton;
   [Export] public TextureRect ItemTexture;
   [Export] public float Margin = 50;
   public ItemData ItemData;
   private bool _isMouseOnTopBar;
   private bool _isDragging;
   private bool _canWindowMove;

   private System.Action _setMouseOnTopBarTrue;
   private System.Action _setMouseOnTopBarFalse;
   
   public override void _Ready()
   {
      _setMouseOnTopBarTrue = () => SetMouseOnTopBar(true);
      _setMouseOnTopBarFalse = () => SetMouseOnTopBar(false);
      TopBar.MouseEntered += _setMouseOnTopBarTrue;
      TopBar.MouseExited += _setMouseOnTopBarFalse;
      CloseButton.Pressed += CloseTab;
      Configure(ItemData);
   }

   public override void _Process(double delta)
   {
      SetWindowMovement();
   }

   // If mouse entered = true, and click = true -> can drag
   public override void _GuiInput(InputEvent @event)
   {
      if (@event is InputEventMouseButton mouseButton && _isMouseOnTopBar)
      {
         if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
         {
            _isDragging = true;
         }
         if (mouseButton.IsReleased() && mouseButton.ButtonIndex == MouseButton.Left)
         {
            _isDragging = false;
         }
      }

      if (@event is InputEventMouseMotion mouseMotion && _isDragging)
      {
         GlobalPosition += mouseMotion.Relative;
      }
   }

   private void SetMouseOnTopBar(bool value)
   {
      _isMouseOnTopBar = value;
      _isDragging = false;
   }

   private void SetWindowMovement()
   {
      Vector2 windowSize = GetViewportRect().Size;

      float x = Mathf.Clamp(GlobalPosition.X,0 - Margin,windowSize.X - Size.X + Margin);
      float y = Mathf.Clamp(GlobalPosition.Y,0 - Margin,windowSize.Y - Size.Y + Margin);
      
      GlobalPosition = new Vector2(x, y);
   }

   private void Configure(ItemData itemData)
   {
      ItemTexture.Texture = itemData.ItemTexture;
   }

   private void CloseTab()
   {
      if (TopBar != null && CloseButton != null)
      {
         TopBar.MouseEntered -= _setMouseOnTopBarTrue;
         TopBar.MouseExited -= _setMouseOnTopBarFalse;
         CloseButton.Pressed -= CloseTab;
      }
      QueueFree();
   }
}