extends TextureRect

@export var item_res: ItemResource
func _ready() -> void:
	if item_res:
		texture = item_res.item_texture


func _get_drag_data(_at_position: Vector2) -> Variant:
	var data: Dictionary = {
		"item_res" = item_res,
		"origin_node" = self
	}

	var preview_texture := TextureRect.new()
	preview_texture.texture = item_res.item_texture

	var preview = Control.new()
	preview.add_child(preview_texture)
	modulate.a = 0.0

	set_drag_preview(preview)
	return data

func _can_drop_data(_at_position: Vector2, data: Variant) -> bool:
	return data is Dictionary

func _drop_data(_at_position: Vector2, data: Variant) -> void:
	var origin_node = data["origin_node"]
	var dragged_item = data["item_res"]
	
	var current_item_here = item_res
	
	item_res = dragged_item
	texture = item_res.item_texture
	modulate.a = 1
	
	origin_node.item_res = current_item_here
	
	if current_item_here:
		origin_node.texture = current_item_here.item_texture
	else:
		origin_node.texture = null

	origin_node.modulate.a = 1
	
func _notification(what: int) -> void:
	if what == NOTIFICATION_DRAG_END and not is_drag_successful():
		modulate.a = 1
