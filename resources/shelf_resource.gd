extends Resource
class_name ShelfResource

enum shelf_types {
	SQUARE_2X2,
	SQUARE_3X3
}

@export var shelf_type: shelf_types = shelf_types.SQUARE_2X2

# Quando for do tipo 2x2, instanciar itens para cada posição
# Ou seja, adicioanar um control node do tipo "GridContainer", que contenha 2 colunas e sempre um
# espaçamento de 3, tanto na vertical quando na horizontal
#
#
#
#
#
#
#
