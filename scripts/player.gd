extends CharacterBody2D

signal interact_pressed

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
@onready var anim: AnimationPlayer = $AnimationPlayer
@onready var sprite: Sprite2D = $Sprite2D

@export var move_speed = 50

func _process(delta: float) -> void:
	if velocity.x != 0:
		anim.play("move")
	else:
		anim.play("idle")
		
	if Input.is_action_just_pressed("interact"):
		interact_pressed.emit()

func _physics_process(delta: float) -> void:
	velocity.y += gravity * delta
	if velocity.x > 0:
		sprite.flip_h = false
	elif velocity.x < 0:
		sprite.flip_h = true
	movement()
	move_and_slide()
	
func movement() -> void:
	var direction = Input.get_axis("move_left","move_right")
	velocity.x = direction * move_speed
