@tool
extends Node2D
@onready var sprite_2d: Sprite2D = $Sprite2D
@export var texture: Texture2D:
	set(value):
		texture = value
		if sprite_2d: 
			sprite_2d.texture = texture

func _ready() -> void:
	if texture:
		sprite_2d.texture = texture
