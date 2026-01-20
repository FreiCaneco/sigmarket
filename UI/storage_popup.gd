extends Control

@onready var grid: GridContainer = $Storage

func _ready() -> void:
	load_item_slots()
	self.visible = false
	SignalBus.shelf_interacted.connect(open_storage)
	SignalBus.exit_pressed.connect(close_storage)

func open_storage(_shelf_node):
	self.visible = true
	print("Era pra estar aberto")

func close_storage():
	self.visible = false

func load_item_slots() -> void:
	var item = preload("res://scenes/item.tscn")
	for i in range(36):
		var item_instantiated = item.instantiate()
		grid.add_child(item_instantiated)
	
