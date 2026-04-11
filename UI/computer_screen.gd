extends Control
@onready var areas_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/AreasButtons
@onready var items_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/ItemsButtons
@onready var upgrades_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/UpgradesButtons
@onready var sidebar_containers: Array[VBoxContainer] = [areas_sidebar,items_sidebar,upgrades_sidebar]

@onready var sidebar_buttons = ButtonGroup.new()

func _ready() -> void:
	for container in sidebar_containers:
		for button in container.get_children():
			if button is TextureButton:
				button.button_group = sidebar_buttons

func _on_tab_container_tab_changed(tab: int) -> void:
	for i in sidebar_containers.size():
		sidebar_containers[i].visible = (tab == i)
		
