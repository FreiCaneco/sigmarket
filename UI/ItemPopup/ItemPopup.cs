using Godot;
using sigmarket.Scenes.Item;
using sigmarket.Shared.Singletons;

namespace sigmarket.Ui.ItemPopup;

public partial class ItemPopup : PanelContainer
{
   [Signal] public delegate void ItemPopupMovementStartedEventHandler(ItemPopup popup);
   [Signal] public delegate void ItemPopupClosedEventHandler(ItemPopup popup);
   
   [Export] public HBoxContainer TopBar;
   [Export] public TextureButton CloseButton;
   [Export] public TextureRect ItemTexture;
   [Export] public Label ItemPrice;
   [Export] public Label ItemDescription;
   [Export] public TextureButton BuyButton;
   [Export] public float Margin = 50;
   
   public ItemData ItemData;
   private bool _isMouseOnTopBar;
   private bool _isDragging;

   private Tween _openingClosingTween;
   
   [Export] public float StartingTransparency = 0.5f;
   
   private System.Action _setMouseOnTopBarTrue;
   private System.Action _setMouseOnTopBarFalse;
   
   public override void _Ready()
   {
      _setMouseOnTopBarTrue = () => SetMouseOnTopBar(true);
      _setMouseOnTopBarFalse = () => SetMouseOnTopBar(false);
      TopBar.MouseEntered += _setMouseOnTopBarTrue;
      TopBar.MouseExited += _setMouseOnTopBarFalse;
      CloseButton.Pressed += ClosePressed;
      BuyButton.Pressed += BuyItem;
      
      Configure(ItemData);
   }

   public override void _Process(double delta)
   {
      SetWindowMovement();
   }

   // If mouse entered = true, and click = true -> can drag
   public override void _GuiInput(InputEvent @event)
   {
      if (@event is InputEventMouseButton mouseButton)
      {
         if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left && _isMouseOnTopBar)
         {
            _isDragging = true;
            EmitSignal(SignalName.ItemPopupMovementStarted, this);
         }
         if (!mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
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
      ItemPrice.Text = itemData.ItemPrice.ToString();
      ItemDescription.Text = itemData.ItemDescription;
   }

   private void ClosePressed()
   {
      if (TopBar != null && CloseButton != null)
      {
         TopBar.MouseEntered -= _setMouseOnTopBarTrue;
         TopBar.MouseExited -= _setMouseOnTopBarFalse;
         CloseButton.Pressed -= ClosePressed;   
      }
      EmitSignal(SignalName.ItemPopupClosed, this);
   }

   public void OpeningAnimation()
   {
      if(_openingClosingTween != null && _openingClosingTween.IsRunning()) _openingClosingTween.Kill();
      Scale = new Vector2(0.8f,0.8f);
      Color transparencyColor = Modulate;
      transparencyColor.A = StartingTransparency;
      Modulate = transparencyColor;
      
      _openingClosingTween = CreateTween().SetParallel().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Back);
      _openingClosingTween.TweenProperty(this, Control.PropertyName.Scale.ToString(), new Vector2(1, 1),0.2);
      _openingClosingTween.TweenProperty(this, "modulate:a",1f,0.2);
   }
   
   public Tween ClosingAnimation()
   {
      if(_openingClosingTween != null && _openingClosingTween.IsRunning()) _openingClosingTween.Kill();
      
      _openingClosingTween = CreateTween().SetParallel().SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);
      _openingClosingTween.TweenProperty(this, Control.PropertyName.Scale.ToString(), new Vector2(0.7f, 0.7f),0.2);
      _openingClosingTween.TweenProperty(this, "modulate:a",0.3f,0.2);
      return _openingClosingTween;
   }

   public void BuyItem()
   {
      GD.Print("EU TINHA ISSO DE DINHEIRO CARALHO " + GlobalData.Instance.PlayerMoney);
      GlobalData.Instance.PlayerMoney -= ItemData.ItemPrice;
      GD.Print("ESTOU DURO AGORA TENHO ISSO DE DINHEIRO " + GlobalData.Instance.PlayerMoney);
   }
}