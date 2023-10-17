extends Node

#names of different tiles
var GroundDictonary = {
	"grass": 0,
}
var UndergroundDictonary = {
	"white_rock": 0,
	"repolite": 1,
}

var Biomes = [
	{
		"name": "plains",
		"underground":0,
		"ground":0,
		
		"plants":[
			{"plant":"pine","chance":0.00005},
			{"plant":"refledius","chance":0.0005},
			
			{"plant":"gifis_bush","chance":0.0004},
			{"plant":"windwheat","chance":0.0003},
			{"plant":"emberwood","chance":0.0001},
			
			{"plant":"small_grass","chance":0.01},
			{"plant":"clover","chance":0.001},
			{"plant":"perlpetal","chance":0.002},
		],
		"deco":[
			{"deco":"whitestone","chance":0.0002},
		],
		"temperature": [-0.3,0.3],
		"humidity": [-0.3,0],
	},
	{
		"name": "windwheat_valley",
		"underground":1,
		"ground":0,
		"plants":[
			{"plant":"ferchus_tree","chance":0.0001},
			{"plant":"windwheat","chance":0.25},
			],
		"deco":[
			{"deco":"whitestone","chance":0.001},
		],
		"temperature": [-0.3,0.3],
		"humidity": [0.5,1.0],
	},
	{
		"name": "forest",
		"underground":1,
		"ground":0,
		"plants":[
			{"plant":"ferchus_tree","chance":0.002}
		],
		"deco":[
			{"deco":"whitestone","chance":0.001},
		],
		"temperature": [-0.3,0.3],
		"humidity": [0,0.3],
	}	
]



func GenBiomeMap(var Seed, var xoff, var yoff, var chunk_size):
	var BiomeMap = []
	
	var temperature = OpenSimplexNoise.new()
	temperature.seed = Seed + 65464
	temperature.octaves = 2
	temperature.period = 1000.0

	var humidity = OpenSimplexNoise.new()
	humidity.seed = Seed - 23571
	humidity.octaves = 4
	humidity.period = 500.0
	
	for y in range(chunk_size):
		BiomeMap.push_back([])
		for x in range(chunk_size):
			var t = temperature.get_noise_2d(chunk_size*yoff+y,chunk_size*xoff+x)
			var h = humidity.get_noise_2d(chunk_size*yoff+y,chunk_size*xoff+x)
			BiomeMap[y].push_back(null)
			for i in range(Biomes.size()):
				if(t >= Biomes[i]["temperature"][0] and t < Biomes[i]["temperature"][1] and h >= Biomes[i]["humidity"][0] and h < Biomes[i]["humidity"][1]):
					BiomeMap[y][x] = i
					break
			if BiomeMap[y][x] ==null: BiomeMap[y][x] = 0
	return BiomeMap
	
