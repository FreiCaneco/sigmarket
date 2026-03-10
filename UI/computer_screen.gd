extends MarginContainer
@onready var areas_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/AreasButtons
@onready var items_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/ItemsButtons
@onready var upgrades_sidebar: VBoxContainer = $HBoxContainer/SideBar/HBoxContainer/SectionsHolder/UpgradesButtons
@onready var sidebar_containers: Array[VBoxContainer] = [areas_sidebar,items_sidebar,upgrades_sidebar]

@onready var current_sidebar_buttons: Array = [areas_sidebar.get_children()]

func _on_tab_container_tab_changed(tab: int) -> void:
	for i in sidebar_containers.size():
		sidebar_containers[i].visible = (tab == i)
		
		if sidebar_containers[i].visible:  
			current_sidebar_buttons = sidebar_containers[i].get_children()
			for button in current_sidebar_buttons:
				button.pressed.connect(_on_button_pressed)

# Conectar uma função para o sinal de pressed de cada botão
# Se um botão da lista esta pressed os outros não devem estar pressed

func _on_button_pressed() -> void:
	pass
