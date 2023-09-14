extends Control


func _on_New_Game_Button_pressed():
	get_tree().change_scene("res://world/World.tscn")


func _on_Exit_Button_pressed():
	get_tree().quit()

