extends Node2D

func _on_interaction_component_player_interacted() -> void:
	if Global.current_period == Global.period_cycle.NIGHT:
		SignalBus.bed_interacted.emit()
	else:
		print("ta de dia porra")
