extends Control

func _ready():
	$Sounds/SelectSound.play()

func _on_Return_Button_pressed():
	get_tree().change_scene("res://UI/scenes/Main Menu.tscn")
