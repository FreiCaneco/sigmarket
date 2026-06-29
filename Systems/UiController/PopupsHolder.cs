using System.Collections.Generic;
using sigmarket.Ui.ItemPopup;
using Godot;
using sigmarket.Scenes.Item;

namespace sigmarket.Systems.UiController;

public partial class PopupsHolder : Node
{
    [Export] public PackedScene ItemPopupScene { get; set; }
    private List<ItemPopup> _popups = new();
    
    private Vector2 _spawnPosition = new Vector2(600f, 273f);
    private int _currentPopupZIndex = 1;
    private bool _firstTimeSpawning = true;
    
    public void CreatePopup(ItemData itemData)
    {
        if (itemData == null) return; 
        if (ItemPopupScene.Instantiate() is not ItemPopup popup) return;
        popup.ItemData = itemData;
        popup.GlobalPosition = SetSpawnPosition();
        AddChild(popup);
        
        _popups.Add(popup);
        popup.ItemPopupMovementStarted += MovePopupToTheTop;
        popup.ItemPopupClosed += ClosePopup;
        popup.OpeningAnimation();
    }
    
    private async void ClosePopup(ItemPopup popup)
    {
        Tween tween = popup.ClosingAnimation();
        await ToSignal(tween, "finished");
        
        popup.ItemPopupMovementStarted -= MovePopupToTheTop;
        popup.ItemPopupClosed -= ClosePopup;
        _popups.Remove(popup);
        if(_popups.Count == 0) _spawnPosition = new Vector2(600f, 273f);
        popup.QueueFree();
    }
    
    private void MovePopupToTheTop(ItemPopup popup)
    {
        popup.ZIndex = _currentPopupZIndex;
        _currentPopupZIndex += 1;
    }

    private Vector2 SetSpawnPosition()
    {
        if (!_firstTimeSpawning) return _spawnPosition += new Vector2(42f, 18f);
        _firstTimeSpawning = false;
        return _spawnPosition;
    }
}