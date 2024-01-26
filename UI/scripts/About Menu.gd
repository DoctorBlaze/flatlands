extends Control

func _ready():
	$Sounds/SelectSound.play()

func _on_Return_Button_pressed():
	get_tree().change_scene_to_file("res://UI/scenes/Main Menu.tscn")

func _on_Return_Button_mouse_entered():
	$Sounds/HoverSound.play()
