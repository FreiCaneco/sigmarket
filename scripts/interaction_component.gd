extends Node2D

@onready var texture_rect: TextureRect = $TextureRect
@onready var texture_original_y = texture_rect.position.y
var hover_tween := create_tween()
var opacity_tween := create_tween()

func _on_area_2d_body_entered(body: Node2D) -> void:
	if body.is_in_group("Player"):
		reset_tweens()
		start_hover_animation()

func _on_area_2d_body_exited(body: Node2D) -> void:
	if body.is_in_group("Player"):
		reset_tweens()
		opacity_tween = create_tween()
		opacity_tween.tween_property(texture_rect,"modulate:a",0,0.1)

func start_hover_animation() -> void:
	opacity_tween = create_tween()
	opacity_tween.tween_property(texture_rect,"modulate:a",1,0.1)
	
	hover_tween = create_tween()
	hover_tween.set_loops()
	hover_tween.set_trans(hover_tween.TRANS_SINE).set_ease(hover_tween.EASE_IN_OUT)
	hover_tween.tween_property(texture_rect,"position:y",texture_original_y - 2,0.3)
	hover_tween.tween_property(texture_rect,"position:y",texture_original_y,0.3)

func reset_tweens() -> void:
	if hover_tween:
		hover_tween.kill()
	if opacity_tween:
		opacity_tween.kill()
