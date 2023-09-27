extends Node

var is_paused = false
var SelectedWorld = ""

func sleep(time):
	while(time > 0):
		time -= 0.01
	return true
