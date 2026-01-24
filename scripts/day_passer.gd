extends Node2D

@onready var timer: Timer = $Timer

var is_open = false
var current_hour = 8
var max_hour = 10

func _ready() -> void:
	SignalBus.bed_interacted.connect(change_time)
	SignalBus.door_interacted.connect(open_market)
	timer.autostart = false
	
func change_time() -> void:
	if Global.current_period == Global.period_cycle.DAY:
		Global.current_period = Global.period_cycle.DAY
	else:
		Global.current_period = Global.period_cycle.DAY
		Global.current_day += 1
		print(Global.current_day)
		
func _on_timer_timeout() -> void:
	if current_hour < max_hour:
		current_hour += 1
		timer.start()
		print("Horario atual: " + str(current_hour))
		print("Horario max: " + str(max_hour))
	else:
		print("dia acabou")
		timer.stop()

func open_market() -> void:
	is_open = true
	timer.start()
