extends Node2D
const Chunk = preload("res://world/Chunk.gd")

const Seasons = ["spring","summer","autumn","winter"]
enum DayCycle {Morning,Day,Evening,Night}

#in seconds
const DayLength = 1200
const SeasonLength = 20 #in days
var WorldTime = 0

#world gen 
const ChunkSize = 4 #in tiles
const WorldScaling = 4
var Chunks = []
var ChunkStatus = []


#file loading and saving
var WorldName = "cummers"

var Player



func SaveWorld():
	var cfg = File.new()
	var cfgStr = "{\n"
	var dir = Directory.new()
	dir.open("res://_run/worlds/")
	if(!dir.dir_exists(WorldName)): dir.make_dir(WorldName)
	
	cfgStr += '"WorldName" : "' + WorldName + '",\n'
	cfgStr += '"DayLength" : ' + str(DayLength) + ',\n'
	cfgStr += '"SeasonLength" : ' + str(SeasonLength) + ',\n'
	cfgStr += '"ChunkSize" : ' + str(ChunkSize) + ',\n'
	cfgStr += '"WorldScaling" : ' + str(WorldScaling) + ',\n'
	cfgStr += '\n}'
	cfg.open("res://_run/worlds/" + WorldName + "/config.json", cfg.WRITE)
	assert(cfg.is_open())
	cfg.store_string(cfgStr)
	cfg.close()

	var tm = File.new()
	tm.open("res://_run/worlds/" + WorldName + "/time", cfg.WRITE)
	tm.store_string(str(WorldTime))
	tm.close()




#uptates world time
func _on_SecondTimer_timeout():
	WorldTime += 1
	LoadChunksUnderPlayer()


func _ready():
	SaveWorld()
	Player = $MainCharacter

var plant_search_radius = 2
var selected_plant = -1

func get_plant_at(x,y):
	var res = -1
	var tmpi = 0
	var tmpj = 0
	for i in range(x-plant_search_radius+1,x+plant_search_radius+1):
		for j in range(y-plant_search_radius+1,y+plant_search_radius+1):
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


#worldgen ------------------------------------------------------------------------------------------
#get chunk by coords (in chunks)
func getChunk(x,y):
	for c in Chunks:
		if c != null and c.x == x and c.y == y:
			return c
	return null
	
#get chunk by coords (in chunks)
func removeChunk(x,y):
	for i in range(Chunks.size()):
		if Chunks[i].x == x and Chunks[i].y == y:
			Chunks.remove(i)
			return

func getChunkStatus(x,y):
	for c in ChunkStatus:
		if c.x == x and c.y == y:
			return c.z
	return -1
		

func ParseChunkTiles(x_chunk_off,y_chunk_off,MainMap,ToplayerMap):
	var xoff = ChunkSize * x_chunk_off
	var yoff = ChunkSize * y_chunk_off
	for y in range(ChunkSize):
		for x in range(ChunkSize):
			#if(x==0 and y==0): $Mainmap.set_cell(xoff+x, yoff+y, -1)
			$Mainmap.set_cell(xoff+x, yoff+y, 0)
			$Toplayer.set_cell(xoff+x,yoff+y,-1)


func RemoveChunkTiles(x_chunk_off,y_chunk_off):
	var xoff = ChunkSize * x_chunk_off
	var yoff = ChunkSize * y_chunk_off
	for y in range(ChunkSize):
		for x in range(ChunkSize):
			$Mainmap.set_cell(xoff+x,yoff+y,-1)
			$Toplayer.set_cell(xoff+x,yoff+y,-1)


#creates the objects of the chunk and tries to parse it
func TryLoadChunk(x,y):
	if(getChunk(x,y)==null):
		print("gen! chunk num: ", Chunks.size())
		var ch = Chunk.new()
		ch.x = x
		ch.y = y
		ch.ParentWorld = self
		ch.chunk_size = ChunkSize
		ch.GenChunk()
		ch.ParseChunkTiles()
		Chunks.push_back(ch)


func LoadChunksUnderPlayer():
	$Mainmap.update_bitmask_region()
	var px = int((Player.position.x / 32) / ChunkSize)
	var py = int((Player.position.y / 32) / ChunkSize)
	
	for i in range(-1,3):
		for j in range(-1,3):
			TryLoadChunk(px+i-1,py+j-1)
	
	var idx = []
	
	var has_garbage = true
	
	while has_garbage: 
		for i in range(Chunks.size()):
			if abs(Chunks[i].x - px) > 2 or abs(Chunks[i].y - py) > 2:
				Chunks[i].RemoveChunkTiles()
				Chunks[i].queue_free()
				Chunks.remove(i)
				has_garbage = true
				break
			else: has_garbage = false
		

