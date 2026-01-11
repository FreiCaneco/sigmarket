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

func _on_player_interacted() -> void:
	InteractionEvents.shelf_interacted.emit(self)

# Lembre-se de calcular a posição da camera position baseado no tamanho do atlas.
