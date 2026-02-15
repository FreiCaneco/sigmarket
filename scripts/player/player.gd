extends CharacterBody2D

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
@onready var anim: AnimationPlayer = $AnimationPlayer
@onready var sprite: Sprite2D = $Sprite2D
@onready var audio: AudioStreamPlayer = $AudioStreamPlayer

@export var move_speed = 50

func _ready() -> void:
	Global.player = self

func _process(_delta: float) -> void:
	if velocity.x != 0:
		anim.play("move")
		if not audio.playing:
			audio.play()
	else:
		anim.play("idle")
		audio.stop()

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
