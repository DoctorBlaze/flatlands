extends Node2D
var plant_search_radius = 1
var selected_plant = -1




func get_plant_at(x,y):
	var res = -1
	var tmpi = 0
	var tmpj = 0
	for i in range(x-plant_search_radius,x+plant_search_radius):
		for j in range(y-plant_search_radius,y+plant_search_radius):
			var tmp = $Plants.get_cell(i,j)
			if tmp != -1:
				if res == -1 or (res != -1 and (i-x)*(i-x)+(j-y)*(j-y) < (tmpi-x)*(tmpi-x)+(tmpj-y)*(tmpj-y)):
					res = tmp
					tmpi = i
					tmpj = j
	selected_plant=res
	
	if(res != -1):
		var plant_name = $Plants.tile_set.tile_get_texture(res).resource_path.substr(26).replace(".png","")
		return plant_name
	return null
	
	
func get_interactable_at(x,y):
	var res = -1
	var tmpi = 0
	var tmpj = 0
	for i in range(x-plant_search_radius,x+plant_search_radius):
		for j in range(y-plant_search_radius,y+plant_search_radius):
			var tmp = $Plants.get_cell(i,j)
			if tmp != -1:
				if res == -1 or (res != -1 and (i-x)*(i-x)+(j-y)*(j-y) < (tmpi-x)*(tmpi-x)+(tmpj-y)*(tmpj-y)):
					res = tmp
					tmpi = i
					tmpj = j
	selected_plant=res
	
	if(res != -1):
		var inter_name = $InteractableTiles.tile_set.tile_get_texture(res).resource_path.substr(25).replace(".png","")
		return inter_name
	return null
