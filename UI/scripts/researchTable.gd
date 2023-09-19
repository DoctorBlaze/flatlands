extends Panel

var player
var samples = []

func _input(event):
	if event is InputEventMouseButton:
		for i in range(samples.size()):
			if $ScrollContainer/VBoxContainer.get_children()[i].pressed:
				var res = player.make_research(i)
				$PCouter.text = str(player.paper)
				if res: 
					samples.remove(i)
					$ScrollContainer/VBoxContainer.get_children()[i].queue_free()
				return


func open_ui(plr,smpl):
	close_ui()
	set_process_input(true)
	player = plr
	samples = smpl
	if samples.size() == 0: $NoSamples.visible = true
	visible = true
	mouse_filter = MOUSE_FILTER_STOP
	for sample in samples:
		var btn = Button.new()
		btn.text = sample
		btn.flat = true
		$ScrollContainer/VBoxContainer.add_child(btn)
	$PCouter.text = str(player.paper)
	
func close_ui():
	$NoSamples.visible = false
	set_process_input(false)
	samples = []
	for i in $ScrollContainer/VBoxContainer.get_children():
		i.queue_free()
	visible = false
	mouse_filter = MOUSE_FILTER_IGNORE


func _on_Button_pressed():
	close_ui()
	

