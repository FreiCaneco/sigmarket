extends Node2D



func _on_interaction_component_player_interacted() -> void:
	SignalBus.door_interacted.emit()
