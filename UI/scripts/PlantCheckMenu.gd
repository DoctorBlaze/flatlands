extends Control

var player
var selected_plant


func open_ui(plr,sel_plant):
	player = plr
	selected_plant = sel_plant
	
	visible = true
	$Book/PlantName.text = selected_plant.get("name")
	$Book/OterInfo.text = selected_plant.get("short_desc")
	
	if(player.learned_plants.has(selected_plant.get("name"))):
		$Book/InteractOptions.mouse_filter = Control.MOUSE_FILTER_STOP
		$Book/SampleLabel.visible = false
		$Book/InteractOptions.visible = true
	else:
		$Book/InteractOptions.mouse_filter = Control.MOUSE_FILTER_IGNORE
		$Book/SampleLabel.visible = true
		$Book/InteractOptions.visible = false
		$Book/SampleLabel.text = "You collected this sample. Now you can learn it on the research table. After learning the plant, you will be able to collect materials."


	

func _on_CollectMaterials_pressed():
	player.collect_material(selected_plant)


func _on_CheckStatus_pressed():
	pass # Replace with function body.
