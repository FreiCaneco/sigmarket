extends Control

@onready var grid: GridContainer = $Storage
@export var storage: Inventory

func _ready() -> void:
	load_item_slots()
	self.visible = false
	SignalBus.shelf_interacted.connect(open_storage)
	SignalBus.player_stopped_interacting.connect(close_storage)

func open_storage(_shelf_node):
	self.visible = true

func close_storage():
	self.visible = false

func load_item_slots() -> void:
	var item = preload("res://scenes/item.tscn")
	for i in range(storage.items.size()):
		var item_instance = item.instantiate()
		item_instance.item_res = storage.items[i]
		grid.add_child(item_instance)
		
