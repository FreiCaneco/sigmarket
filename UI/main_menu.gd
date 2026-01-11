extends Control

signal play_pressed

func _on_play_button_pressed() -> void:
	play_pressed.emit()
