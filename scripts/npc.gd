extends CharacterBody2D

@onready var sprite: Sprite2D = $Sprite
@export var npc_res: NpcResource:
	set(value):
		npc_res = value
		if is_node_ready() and sprite:
			sprite.texture = npc_res.npc_sprite
			print("Era pra estar aqui!")

func initialize_npc(npc_resource: NpcResource):
	npc_res = npc_resource
	sprite.texture = npc_res.npc_sprite
