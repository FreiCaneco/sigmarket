extends Node2D

# Adicionar uma seção ao interagir simplesmente isso
@onready var sections = get_tree().get_first_node_in_group("Sections")

func add_section() -> void:
	var new_section = preload("res://scenes/sections/yellow_checkered.tscn")
	var section: Node2D = new_section.instantiate()
	section.position = Global.final_pixel_pos
	print(section.position)
	Global.final_pixel_pos += Vector2(160,0)
	SignalBus.last_pixel_changed.emit()
	sections.add_child(section)


func _on_interaction_component_player_interacted() -> void:
	add_section()
