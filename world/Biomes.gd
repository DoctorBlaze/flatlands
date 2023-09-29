extends Node

var Biomes = [
	{
		"name": "plains",
		"underground":0,
		"top_layer":"grass",
		"temperature": [-0.3,0.3],
		"humidity": [-0.3,0],
	},
	{
		"name": "forest",
		"underground":1,
		"top_layer":"grass",
		"temperature": [-0.3,0.3],
		"humidity": [0,0.3],
	}	
]

func GenBiomeMap(var Seed, var xoff, var yoff, var chunk_size):
	var BiomeMap = []
	
	var temperature = OpenSimplexNoise.new()
	temperature.seed = Seed + 65464
	temperature.octaves = 2
	temperature.period = 800.0

	var humidity = OpenSimplexNoise.new()
	humidity.seed = Seed - 23571
	humidity.octaves = 2
	humidity.period = 400.0
	
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
	
