extends Node2D

var ParentWorld
var chunk_size

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
	
	GenUnderground()


func GenUnderground():
	
	var temperature = OpenSimplexNoise.new()
	temperature.seed = ParentWorld.Seed + 65464
	temperature.octaves = 2
	temperature.period = 400.0

	
	var humidity = OpenSimplexNoise.new()
	humidity.seed = ParentWorld.Seed + 2357
	humidity.octaves = 2
	humidity.period = 200.0
	
	BiomeMap = Biomes.GenBiomeMap(ParentWorld.Seed,x,y,chunk_size)

	for yl in range(chunk_size):
		Underground.push_back([])
		for xl in range(chunk_size):
				Underground[yl].push_back(Biomes.Biomes[BiomeMap[yl][xl]]["underground"])
