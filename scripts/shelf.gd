@tool
extends Node2D

@onready var sprite_2d: Sprite2D = $Sprite2D
@onready var camera_position: Marker2D = $CameraPosition

@export var texture: AtlasTexture:
	set(value):
		texture = value
		if sprite_2d: 
			sprite_2d.texture = texture

var texture_size: Vector2

func _ready() -> void:
	if texture:
		sprite_2d.texture = texture
	
	texture_size = texture.region.size

func _on_player_interacted() -> void:
	InteractionEvents.shelf_interacted.emit(self)
		
