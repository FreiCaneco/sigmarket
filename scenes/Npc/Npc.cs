using Godot;

namespace sigmarket.Scenes.Npc;

public partial class Npc : CharacterBody2D
{
    private Sprite2D _sprite;
    private NpcData _npcData;
    [Export] public NpcData NpcData
    {
        get => _npcData;
        set
        {
            _npcData = value;
            if (IsNodeReady() && _sprite != null)
            {
                _sprite.Texture = _npcData.NpcSprite;
            }
        }
    }

    public void InitializeNpc(NpcData npcData)
    {
        NpcData = npcData;
        _sprite.Texture = NpcData.NpcSprite;
    }
}