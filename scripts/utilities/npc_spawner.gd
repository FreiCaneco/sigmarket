extends Node2D

@onready var spawn_timer: Timer = $NpcSpawnTimer
@export var npc_resources: Array[NpcResource]

var min_chance_to_npc_spawn: float = 0.3
var random_npc_res: NpcResource
var npcs_group: Node2D

func _ready() -> void:
	spawn_timer.timeout.connect(spawn_npc)
	npcs_group = get_tree().get_first_node_in_group("Npcs")

func _process(_delta: float) -> void:
	if spawn_timer.is_stopped():
		spawn_timer.start()

# Seleciona um npc aleatório
func spawn_npc() -> void:
	var npc_spawn_chance := randf()
	if npc_spawn_chance >= min_chance_to_npc_spawn:
		random_npc_res = npc_resources.pick_random()
		
		var npc_scene = load("res://scenes/npc.tscn")
		var npc = npc_scene.instantiate()
		npc.position = global_position
		npcs_group.add_child(npc)
		if npc.has_method("initialize_npc"):
			npc.initialize_npc(random_npc_res)
