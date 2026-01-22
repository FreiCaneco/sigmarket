extends Control

@onready var grid: GridContainer = $Storage
var storage: Inventory

func _ready() -> void:
	load_storage_resource()	
	load_item_slots()
	self.visible = false
	SignalBus.shelf_interacted.connect(open_storage)
	SignalBus.player_stopped_interacting.connect(close_storage)

func open_storage(_shelf_node):
	self.visible = true

func close_storage():
	self.visible = false
	ResourceSaver.save(storage,"user://storage.tres",ResourceSaver.FLAG_REPLACE_SUBRESOURCE_PATHS)

func load_item_slots() -> void:
	var item = preload("res://scenes/item.tscn")
	for i in range(storage.items.size()):
		var item_instance = item.instantiate()
		item_instance.item_res = storage.items[i]
		grid.add_child(item_instance)
		
func load_storage_resource() -> void:
	if ResourceLoader.exists("user://storage.tres"):
		storage = ResourceLoader.load("user://storage.tres")
	else:
		var template = ResourceLoader.load("res://resources/storage.tres")
		storage = template.duplicate()
