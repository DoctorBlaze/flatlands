extends Area2D

var direction = Vector2(100,0)

func _physics_process(delta):
	position += direction*delta

func _on_body_entered():
	print("gfgfgfgfgfg")
