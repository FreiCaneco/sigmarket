extends Node2D

@onready var sub_viewport: SubViewport = $SubViewportContainer/SubViewport
@onready var canvas_layer: CanvasLayer = $CanvasLayer

func _ready() -> void:
	SignalBus.computer_interacted.connect(_on_computer_interacted)

func _unhandled_input(event: InputEvent) -> void:
	if Input.is_action_just_pressed("exit") and canvas_layer.visible:
		exit_computer()

func _on_main_menu_play_pressed() -> void:
	var game_scene = load("res://scenes/game.tscn").instantiate()
	var computer_menu_scene = load("res://ui/computer_screen.tscn").instantiate()
	
	sub_viewport.get_child(0).queue_free()
	sub_viewport.add_child(game_scene)
	
	canvas_layer.add_child(computer_menu_scene)
	computer_menu_scene.visible = false
	
func _on_computer_interacted() -> void:
	get_tree().paused = true
	canvas_layer.show()
	
func exit_computer() -> void:
	get_tree().paused = false
	canvas_layer.hide()
