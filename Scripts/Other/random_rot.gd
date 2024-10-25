extends Node2D

func _ready() -> void:
	RandomRot()

func RandomRot():
	var random = RandomNumberGenerator.new()
	print("test")
	rotate(random.randi_range(0, 360))
	print(rotation)

	
