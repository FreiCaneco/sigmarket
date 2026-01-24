extends Node

enum period_cycle {
	DAY,
	NIGHT
}

var cam: Camera2D
var player: Node2D
var current_day: int = 1
var current_period: period_cycle
