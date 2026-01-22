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
	ui_canvas = get_tree().get_first_node_in_group("UI")
	if texture:
		sprite_2d.texture = texture
	
	add_slots_based_on_type()

func _process(_delta: float) -> void:
	if slots_grid and slots_grid.visible:
		var shelf_screen_pos = get_global_transform_with_canvas().origin
		var z = Global.cam.zoom.x
		# Precisa transformar isso em dinamico, pois esse valor dep
		var grid_offset = Vector2(-9, -12)
		
		slots_grid.global_position = (shelf_screen_pos + (grid_offset * z)).round()
		slots_grid.scale = Vector2(z, z)

func add_slots_based_on_type() -> void:
	var grid := GridContainer.new()
	var item = load("res://scenes/item.tscn")
	var numOfSlots: int
	grid.add_theme_constant_override("h_separation",2)
	grid.add_theme_constant_override("v_separation",2)
	grid.mouse_filter = Control.MOUSE_FILTER_PASS
	
	match shelf_res.shelf_type:
		shelf_res.shelf_types.SQUARE_2X2:
			grid.columns = 2
			grid.size = Vector2(18,18)
			grid.position = Vector2(-9,-12)
			numOfSlots = 4
			slots_grid = grid
			ui_canvas.add_child(grid)
			for i in range(numOfSlots):
				var new_item = item.instantiate()
				grid.add_child(new_item)

func _on_player_interaction() -> void:
	SignalBus.shelf_interacted.emit(self)
