extends Node2D

signal player_interacted(player: Node2D)
var player_in_area := false

@onready var texture_rect: TextureRect = $TextureRect
@onready var texture_original_y = texture_rect.position.y
var hover_tween: Tween
var opacity_tween: Tween

var interacting: bool = false

func _ready() -> void:
	set_process(false)

func _process(delta: float) -> void:
	if player_in_area and Input.is_action_just_pressed("interact"):
		player_interacted.emit()
		interaction_animation()
		interacting = true
			
	if Input.is_action_just_pressed("exit") and interacting == true:
		InteractionEvents.exit_pressed.emit()
		interacting = false

func _on_area_2d_body_entered(body: Node2D) -> void:
	if body.is_in_group("Player"):
		set_process(true)
		reset_tweens()
		start_hover_animation()
		player_in_area = true
	

func _on_area_2d_body_exited(body: Node2D) -> void:
	if body.is_in_group("Player"):
		set_process(false)
		reset_tweens()
		clear_opacity()
		player_in_area = false
		interacting = false

func start_hover_animation() -> void:
	opacity_tween = create_tween()
	opacity_tween.tween_property(texture_rect,"modulate:a",1,0.1)
	
	hover_tween = create_tween()
	hover_tween.set_loops()
	hover_tween.set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)
	hover_tween.tween_property(texture_rect,"position:y",texture_original_y - 2,0.3)
	hover_tween.tween_property(texture_rect,"position:y",texture_original_y,0.3)

func interaction_animation() -> void:
	reset_tweens()
	clear_opacity()

func clear_opacity() -> void:
	opacity_tween = create_tween()
	opacity_tween.tween_property(texture_rect,"modulate:a",0,0.1)
	
func reset_tweens() -> void:
	if hover_tween:
		hover_tween.kill()
	if opacity_tween:
		opacity_tween.kill()
