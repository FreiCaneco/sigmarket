extends Node

enum period_cycle {
	DAY,
	NIGHT
}

enum cam_anim_type {
	GO_TO_SHELF,
	GO_TO_PLAYER
}

var cam: Camera2D
var player: Node2D

var current_day: int = 1
var current_period: period_cycle

var final_pixel_pos: Vector2 = Vector2(320,0)
