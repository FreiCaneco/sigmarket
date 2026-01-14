extends Node2D

var player: CharacterBody2D
@onready var anim: AnimationPlayer = $"../AnimationPlayer"

func _ready() -> void:
	InteractionEvents.shelf_interacted.connect(shelf_interaction)
	InteractionEvents.exit_pressed.connect(shelf_exit)
	player = get_parent()

func shelf_interaction(_shelf) -> void:
	player.velocity = Vector2(0,0)
	player.set_physics_process(false)
	var tween := create_tween()
	tween.tween_property(player,"modulate:a",0.2,0.5)
	
func shelf_exit() -> void:
	var tween := create_tween()
	tween.tween_property(player,"modulate:a",1,0.5)
	await tween.finished
	player.set_physics_process(true)
