using Godot;

namespace sigmarket.Ui.LojinhaLegal.Scripts;

public partial class CartListPiece : PanelContainer
{
	private enum BlackOrWhite { Black, White }
	
	private void ChangeStyle(bool isEven)
	{
		if (GetThemeStylebox("panel") is not StyleBoxTexture currentStyle) return;
		if (currentStyle.Duplicate() is not StyleBoxTexture uniqueStyle) return;
		Rect2 temp = uniqueStyle.RegionRect;
		temp.Position = (isEven) ? new Vector2(16,88) : new Vector2(16,80);	
		uniqueStyle.RegionRect = temp;
		AddThemeStyleboxOverride("panel", uniqueStyle);
	}
}