extends MarginContainer
@onready var areas_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/AreasButtons
@onready var items_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/ItemsButtons
@onready var upgrades_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/UpgradesButtons
@onready var sidebar_containers: Array[VBoxContainer] = [areas_sidebar,items_sidebar,upgrades_sidebar]

@onready var current_sidebar_buttons: Array = [areas_sidebar]

func _on_tab_container_tab_changed(tab: int) -> void:
	for i in sidebar_containers.size():
		sidebar_containers[i].visible = (tab == i)
		if sidebar_containers[i].visible: 
			current_sidebar_buttons = sidebar_containers[i].get_children()
