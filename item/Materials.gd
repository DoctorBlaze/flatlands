extends Node

var Materials = {
	"amberwood_flower":{
		"name":"Amberwood flower",
		"short_desc":"Big white flower. All heat concentrated at the middle.",
		
		"taste":"Tastes like freakin' fire! What are you eating?",
		"eat_effect":"ignite",
		"hunger":2,
		
		"icon": Image.new().load("res://asset/texture/material/amberwood_flower.png"),
		"alchemy": {"ignis":0}
	},
	"cinnarod_flower":{
		"name":"Cinnarod flower",
		"short_desc":"Light-yellow flower with strong flavour.",
		
		"taste":"Nice scent! Add this to your drink, maybe.",
		"eat_effect":"Relaxed",
		"hunger":1,
		
		"icon": Image.new().load("res://asset/texture/material/cinnarod_flower.png"),
		"alchemy": {"ignis":0}
	},
	
	
	"refledius_stem":{
		"name":"Refledius stem",
		"short_desc":"A lot of small fibers make it hard to tear. Maybe, it will become light enough, if you'll dry it.",
		
		"taste":"Tastes like a grass.",
		"eat_effect":null,
		"hunger":0,
		
		"icon": Image.new().load("res://asset/texture/material/refledius_stem.png"),
		"alchemy": null
	} 
}

