extends Control

var WorldName
var cfgStr

var DayLength
var SeasonLength

var ChunkSize

func _input(event):
	if event == null: return
	$VBoxContainer/ChunkSizeLabel.text = "\n Chunk size (in tiles): " + str($VBoxContainer/ChunkSizeSlider.value)
	$VBoxContainer/DayLengthLabel.text = "\n Day length (in seconds): " + str($VBoxContainer/DayLengthSlider.value)
	$VBoxContainer/SeasonLengthLabel.text = "\n Season length (in game days): " + str($VBoxContainer/SeasonLengthSlider.value)
	
	


func _on_CreateWorld_pressed():
	WorldName = $WorldNamePanel/WorldName.text
	ChunkSize = $VBoxContainer/ChunkSizeSlider.value
	DayLength = $VBoxContainer/ChunkSizeSlider.value
	SeasonLength = $VBoxContainer/SeasonLengthSlider.value
	
	var cfg = File.new()
	var cfgStr = "{\n"
	var dir = Directory.new()
	dir.open("res://_run/worlds/")
	if(!dir.dir_exists(WorldName)): dir.make_dir(WorldName)
	
	cfgStr += '"WorldName" : "' + WorldName + '",\n'
	cfgStr += '"DayLength" : ' + str(DayLength) + ',\n'
	cfgStr += '"SeasonLength" : ' + str(SeasonLength) + ',\n'
	cfgStr += '"ChunkSize" : ' + str(ChunkSize) + ',\n'
	cfgStr += '\n}'
	cfg.open("res://_run/worlds/" + WorldName + "/config.json", cfg.WRITE)
	assert(cfg.is_open())
	cfg.store_string(cfgStr)
	cfg.close()
	global.SelectedWorld = WorldName
	get_tree().change_scene("res://world/World.tscn")
	


func _on_BackButton_pressed():
	get_tree().change_scene("res://UI/scenes/Main Menu.tscn")
