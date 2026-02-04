extends Camera2D

func _enter_tree() -> void:
	Global.cam = self
	
func _ready() -> void:
	SignalBus.shelf_interacted.connect(go_to_shelf)
	SignalBus.player_stopped_interacting.connect(go_to_player)
		 
func go_to_shelf(shelf: Node2D) -> void:
	var correct_position: Marker2D = shelf.get_node("CameraPosition")
	
	var tween := create_tween()
	tween.set_ease(Tween.EASE_IN_OUT).set_trans(Tween.TRANS_SINE)
	tween.set_parallel(true)
	tween.tween_property(self,"global_position",correct_position.global_position,1)
	tween.tween_property(self,"zoom",Vector2(2,2),1)
	SignalBus.camera_animation_finished.emit()
	
func go_to_player() -> void:
	var tween := create_tween()
	tween.set_ease(Tween.EASE_IN_OUT).set_trans(Tween.TRANS_SINE)
	tween.set_parallel(true)
	tween.tween_property(self,"global_position",Global.player.global_position,0.5)
	tween.tween_property(self,"zoom",Vector2(1,1),0.5)
	SignalBus.camera_animation_finished.emit()
