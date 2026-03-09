extends Node2D

@onready var sub_viewport: SubViewport = $SubViewportContainer/SubViewport
@onready var canvas_layer: CanvasLayer = $CanvasLayer

func _on_main_menu_play_pressed() -> void:
	print("Menu foi pressionado")
	var game_scene = load("res://scenes/game.tscn").instantiate()
	var computer_menu_scene = load("res://ui/computer_screen.tscn").instantiate()
	
	sub_viewport.get_child(0).queue_free()
	sub_viewport.add_child(game_scene)
	
	canvas_layer.add_child(computer_menu_scene)
	computer_menu_scene.visible = false
