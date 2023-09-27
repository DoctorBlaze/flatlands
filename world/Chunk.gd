extends Node2D

var ParentWorld
var chunk_size

#position (in chunks) of the chunk on the map
var x
var y

#generated chunk variables
var MainMap = []
var ToplayerMap = []
#var Decorations = []


func _ready():
	pass


#parse and remove chunk fron generated or loaded data ---------------------------------------------------------
func ParseChunkTiles():
	if MainMap == null: return
	ParentWorld.ParseChunkTiles(x,y,MainMap,ToplayerMap)
	
func RemoveChunkTiles():
	if MainMap == null: return
	ParentWorld.RemoveChunkTiles(x,y)
	


#first chunk generation ----------------------------------------------------------------------------
#only transforms noise data into generated data. Chunk will be created, but not rendered
func GenChunk():
	MainMap = []
	ToplayerMap = []
	for y in range(chunk_size):
		MainMap.push_back([])
		ToplayerMap.push_back([])
		for x in range(chunk_size):
			MainMap[y].push_back(0)
			MainMap[y].push_back(-1)
