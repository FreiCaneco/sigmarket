using Godot;
using sigmarket.Scenes.Npc;

namespace sigmarket.Systems.NpcSpawner;

public partial class NpcSpawner : Node2D
{
    private Timer _spawnTimer;
    private Godot.Collections.Array<NpcData> _npcsData;

    private float _minChanceToSpawnNpc = 0.3f;
    private NpcData _randomNpcData;
    private Node2D _npcsGroup;
    
    private PackedScene _npcScene = GD.Load<PackedScene>("res://Scenes/Npc/npc.tscn");
    public override void _Ready()
    {
        _spawnTimer = GetNode<Timer>("NpcSpawnTimer");
        _npcsGroup = (Node2D)GetTree().GetFirstNodeInGroup("Npcs");
        
        _spawnTimer.Timeout += SpawnNpc;
    }

    public override void _Process(double delta)
    {
        if (_spawnTimer.IsStopped())
        {
            _spawnTimer.Start();
        }
    }

    private void SpawnNpc()
    {
        float npcSpawnChance = GD.Randf();
        if (npcSpawnChance >= _minChanceToSpawnNpc)
        {
            _randomNpcData = _npcsData.PickRandom();
            Npc npc = (Npc)_npcScene.Instantiate();
            npc.Position = GlobalPosition;
            _npcsGroup.AddChild(npc);
            npc.InitializeNpc(_randomNpcData);
        }
    }
}