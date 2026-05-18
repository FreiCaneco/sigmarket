using Godot;

namespace sigmarket.Scenes.Npc;

[GlobalClass]
public partial class NpcData : Resource
{
    [Export] public string NpcName { get; set; }
    [Export] public Texture2D NpcSprite { get; set; }

    public NpcData() : this(null, null){}
    
    public NpcData(string npcName, Texture2D npcSprite)
    {
        NpcName = npcName;
        NpcSprite = npcSprite;
    }
}