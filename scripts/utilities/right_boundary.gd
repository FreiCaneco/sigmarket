extends StaticBody2D

func _ready() -> void:
	SignalBus.last_pixel_changed.connect(on_new_section_created)
	
func on_new_section_created() -> void: 
	position = Global.final_pixel_pos
