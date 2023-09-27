extends Control

var files = []

func _ready():
	var dir = Directory.new()
	dir.open("res://_run/worlds")
	dir.list_dir_begin()

	while true:
		var f = dir.get_next()
		if f == "":
			break
		var tmpdir = Directory.new()
		if f.find(".") == -1 and dir.dir_exists("res://_run/worlds/"+f):	files.push_back(f)

	dir.list_dir_end()
	for f in files:
		var btn = Button.new()
		btn.flat = true
		btn.text = f
		$WorldListCont/WorldList.add_child(btn)


func _input(event):
	if event is InputEventMouseButton:
		for i in range(files.size()):
			if $WorldListCont/WorldList.get_children()[i].pressed:
				global.SelectedWorld = $WorldListCont/WorldList.get_children()[i].text
				$VBoxContainer2/PlayWorld.disabled = false
				return



func _on_PlayWorld_pressed():
	if global.SelectedWorld == "": return
	get_tree().change_scene("res://world/World.tscn")


func _on_BackButton_pressed():
	get_tree().change_scene("res://UI/scenes/Main Menu.tscn")
