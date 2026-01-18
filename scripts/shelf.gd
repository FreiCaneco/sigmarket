@tool
extends Node2D

@onready var sprite_2d: Sprite2D = $Sprite2D
@onready var camera_position: Marker2D = $CameraPosition

@export var shelf_res: shelf_resource
@export var texture: AtlasTexture:
	set(value):
		texture = value
		if sprite_2d: 
			sprite_2d.texture = texture

func _ready() -> void:
	if texture:
		sprite_2d.texture = texture
	
	add_slots_based_on_type()

func _on_player_interacted() -> void:
	InteractionEvents.shelf_interacted.emit(self)
	
func add_slots_based_on_type() -> void:
	var numOfSlots: int
	var grid := GridContainer.new()
	grid.add_theme_constant_override("h_separation",2)
	grid.add_theme_constant_override("v_separation",2)
	grid.mouse_filter = Control.MOUSE_FILTER_PASS
	
	match shelf_res.shelf_type:
		shelf_resource.shelf_types.SQUARE_2X2:
			grid.columns = 2
			grid.size = Vector2(18,18)
			grid.position = Vector2(-9,-12)
			numOfSlots = 4
			add_child(grid)
			for i in range(numOfSlots):
				pass
				#var item = load("res://scenes/item.tscn")
				#item = item.instantiate()
				#grid.add_child(item)
