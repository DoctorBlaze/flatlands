extends Node

var ParentWorld
var chunk_size

var ChunkPlants

#position (in chunks) of the chunk on the map
var x
var y

#generated chunk variables
var Underground = null
var Ground = null
var Surface = null

var BiomeMap


func _ready():
	pass


#parse and remove chunk fron generated or loaded data ----------------------------------------------
func ParseChunkTiles():
	if Underground == null: return
	ParentWorld.ParseChunkTiles(x,y,self)
	
func RemoveChunkTiles():
	ParentWorld.RemoveChunkTiles(x,y)
	

#first chunk generation ----------------------------------------------------------------------------
#only transforms noise data into generated data. Chunk will be created, but not rendered
func GenChunk():
	Underground = []
	Ground = []
	Surface = []
	
	GenMainMap()
	GenPlants()


func GenMainMap():
	BiomeMap = Biomes.GenBiomeMap(ParentWorld.Seed,x,y,chunk_size)
	
	var water = OpenSimplexNoise.new()
	water.seed = ParentWorld.Seed/2 - 53471
	water.octaves = 3
	water.period = 80.0
	
	
	for yl in range(chunk_size):
		Underground.push_back([])
		Ground.push_back([])
		for xl in range(chunk_size):
			var biome = Biomes.Biomes[BiomeMap[yl][xl]]
			var w = water.get_noise_2d(chunk_size*y+yl,chunk_size*x+xl)
			if(w > 0.45):
				Ground[yl].push_back(-1)
				Underground[yl].push_back(3)
			else:
				Ground[yl].push_back(biome["ground"])
				Underground[yl].push_back(0)
			


func GenPlants():
	ChunkPlants = []
	var plant_noise = OpenSimplexNoise.new()
	plant_noise.octaves = 4
	plant_noise.period = 100.0
	 
	for yl in range(chunk_size):
		ChunkPlants.push_back([])
		for xl in range(chunk_size):
			if(Ground[yl][xl]!=-1):
				var biome = Biomes.Biomes[BiomeMap[yl][xl]]
				var freeTile = true
				ChunkPlants[yl].push_back(null)
				for plant in biome["plants"]:
					if plant["chance"] > randf():
						ParentWorld.GenPlacePlant(plant["plant"],xl+x*chunk_size,yl+y*chunk_size)
						freeTile = false
						break
				if freeTile:
					for deco in biome["deco"]:
						if deco["chance"] > randf():
							ParentWorld.GenPlaceDeco(deco["deco"],xl+x*chunk_size,yl+y*chunk_size)
							freeTile = false
							break
	
