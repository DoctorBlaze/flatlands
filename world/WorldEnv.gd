extends Node

var Seasons = ["spring","summer","autumn","winter"]
enum DayCycle {Morning,Day,Evening,Night}

#in seconds
var DayLength = 1200
var WorldTime = 0


func _process(delta):
	WorldTime += delta
	print(WorldTime)
