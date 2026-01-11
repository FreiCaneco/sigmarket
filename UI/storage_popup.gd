extends Control

func _ready() -> void:
	self.visible = false
	InteractionEvents.shelf_interacted.connect(open_storage)

func open_storage(_shelf_node):
	self.visible = true
