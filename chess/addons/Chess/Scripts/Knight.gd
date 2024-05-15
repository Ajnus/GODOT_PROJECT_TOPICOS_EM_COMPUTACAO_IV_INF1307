@tool
extends Pawn
class_name Knight

func _ready():
	self.texture = load("res://chess/addons/Chess/Textures/WKnight.svg")

func _process(_delta):
	if Item_Color != Temp_Color:
		Temp_Color = Item_Color
		if Item_Color == 0:
			self.texture = load("res://chess/addons/Chess/Textures/WKnight.svg")
		elif Item_Color == 1:
			self.texture = load("res://chess/addons/Chess/Textures/BKnight.svg")
