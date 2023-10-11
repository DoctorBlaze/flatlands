extends Node


var Plants = {
	"gifis_bush":{
		"name":"gifis bush",
		
		"grow_seasons": [0,1],
		"grow_day_cycle":null,
		
		"short_desc":"Seems like it's a simple bush.",
		
		"default_scene": load("res://plant/plantScenes/GifisBush.tscn"),
	},
	"windwheat":{
		"name":"windwheat",
		
		"grow_seasons": [1,2],
		"grow_day_cycle":null,
		
		"short_desc":"Bruh.",
		
		"default_scene": load("res://plant/plantScenes/Windwheat.tscn"),
	},
	"cinnarod":{
		"name":"cinnarod",
		"short_desc":"Has a pleasant relaxing scent.",
		
		"grow_seasons": [1,2],
		"grow_day_cycle":null,
		
		"material":"cinnarod_flower",
		
		"default_scene": load("res://path/to/scene.tscn"),
	},
	"emberwood":{
		"name":"emberwood",
		"short_desc":"A feel of warmness comes from these red, a little glowy leaves.",
		
		"grow_seasons":[0,1],
		"grow_day_cycle":null,
		
		"material":"amberwood_flower",
		
		"default_scene": load("res://plant/plantScenes/Emberwood.tscn"),
	},
	"ferchus_tree":{
		"name":"ferchus tree",
		"short_desc":"Desc",
		
		"grow_seasons":[0,1,2],
		"grow_days":10,
		"grow_material_days":10,
		"grow_day_cycle":null,
		
		"material":"ferchus_leave",
		
		"default_scene": load("res://plant/plantScenes/FerchusTree.tscn"),
	}
}


#-------------------------------------------------------------------------------

var SmallPlants = {
	"bush":{
		"name":"gifis bush",
		
		"grow_seasons": [0,1],
		"grow_day_cycle":null,
		
		"short_desc":"Seems like it's a simple bush.",
	},
	"cinnarod":{
		"name":"cinnarod",
		"short_desc":"Has a pleasant relaxing scent.",
		
		"grow_seasons": [1,2],
		"grow_day_cycle":null,
		
		"material":"cinnarod_flower"
	},
	"emberwood":{
		"name":"emberwood",
		"short_desc":"A feel of warmness comes from these red, a little glowy leaves.",
		
		"grow_seasons":[0,1],
		"grow_day_cycle":null,
		
		"material":"amberwood_flower"
	},
	"refledius":{
		"name":"refledius",
		"short_desc":"There is very little energy felt from this plant. However, the stem looks strong. Maybe try using it as a rope?",
		
		"grow_seasons":[0,1,2],		
		"grow_day_cycle":null,
		
		"material":"refledius_stem"
	}
}
