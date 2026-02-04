extends Node2D

@onready var sprite_2d: Sprite2D = $Sprite2D
@onready var camera_position: Marker2D = $CameraPosition

@export var shelf_res: ShelfResource
@export var texture: AtlasTexture:
	set(value):
		texture = value
		if sprite_2d: 
			sprite_2d.texture = texture

var ui_canvas: CanvasLayer
var slots_grid: GridContainer

func _ready() -> void:
	SignalBus.camera_animation_finished.connect(_on_camera_finished_animation)
	ui_canvas = get_tree().get_first_node_in_group("UI")
	if texture:
		sprite_2d.texture = texture
	
	add_slots_based_on_type()

func _process(_delta: float) -> void:
	if slots_grid and slots_grid.visible and slots_grid.get_parent() == ui_canvas:
		
		var shelf_screen_pos = get_global_transform_with_canvas().origin
		var z = Global.cam.zoom.x
		# Precisa transformar isso em dinâmico
		var grid_offset = Vector2(-9, -12)
		
		slots_grid.global_position = (shelf_screen_pos + (grid_offset * z))
		slots_grid.scale = Vector2(z, z)
	else:
		var shelf_screen_pos = get_global_transform().origin
		var z = Global.cam.zoom.x
		var grid_offset = Vector2(-9,-12)
		slots_grid.global_position = (shelf_screen_pos + (grid_offset * z))
		slots_grid.scale = Vector2(1, 1)


func add_slots_based_on_type() -> void:
	slots_grid = GridContainer.new()
	add_child(slots_grid)
	var item = load("res://scenes/item.tscn")
	var numOfSlots: int
	slots_grid.add_theme_constant_override("h_separation",2)
	slots_grid.add_theme_constant_override("v_separation",2)
	slots_grid.mouse_filter = Control.MOUSE_FILTER_PASS
	
	match shelf_res.shelf_type:
		shelf_res.shelf_types.SQUARE_2X2:
			slots_grid.columns = 2
			slots_grid.size = Vector2(18,18)
			slots_grid.position = Vector2(-9,-12)
			numOfSlots = 4
			for i in range(numOfSlots):
				var new_item = item.instantiate()
				slots_grid.add_child(new_item)

func _on_player_interaction() -> void:
	SignalBus.shelf_interacted.emit(self)


func _on_camera_finished_animation(anim_type: Global.cam_anim_type)-> void:
	if anim_type == Global.cam_anim_type.GO_TO_SHELF:
		slots_grid.reparent(ui_canvas)
	elif anim_type == Global.cam_anim_type.GO_TO_PLAYER:
		slots_grid.reparent(self)
