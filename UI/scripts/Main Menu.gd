extends Control

const time = 65000

func _ready():
	$Sounds/ReturnSound.play()

# <= Click =>

func _on_New_Game_Button_pressed():
	$Sounds/SelectSound.play()
	global.sleep(time)
	get_tree().change_scene("res://world/World.tscn")


func _on_Continue_Game_Button_pressed():
	$Sounds/LoadSound.play()
	global.sleep(time)
	get_tree().change_scene("res://world/World.tscn")


func _on_Setting_Button_pressed():
	get_tree().change_scene("res://UI/scenes/Settings Menu.tscn")


func _on_Help_Button_pressed():
	get_tree().change_scene("res://UI/scenes/Help Menu.tscn")


func _on_About_Button_pressed():
	get_tree().change_scene("res://UI/scenes/About Menu.tscn")


func _on_Exit_Button_pressed():
	$Sounds/SelectSound.play()
	global.sleep(time)
	get_tree().quit()


# <= Hover =>

func _on_New_Game_Button_mouse_entered():
	$Sounds/HoverSound.play()


func _on_Continue_Game_Button_mouse_entered():
	$Sounds/HoverSound.play()


func _on_Setting_Button_mouse_entered():
	$Sounds/HoverSound.play()


func _on_Help_Button_mouse_entered():
	$Sounds/HoverSound.play()


func _on_About_Button_mouse_entered():
	$Sounds/HoverSound.play()


func _on_Exit_Button_mouse_entered():
	$Sounds/HoverSound.play()
