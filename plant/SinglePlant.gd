extends Area2D

#"Parent" ref
var ListRef

#index on the map
var ind = 0

#time management
var growTime = 0
var growStage = 0
var LastTimeUpdated
var MaterialTime = 0



func init(LRef,i):
	LastTimeUpdated = WorldEnv.WorldTime
	ListRef = LRef
	ind = i
	
func randomUpdate():
	var deltaTime = WorldEnv.WorldTime - LastTimeUpdated
	LastTimeUpdated = WorldEnv.WorldTime
	if(growTime > 0):
		growTime -= deltaTime
		if(growTime <= 0): grow() #do something
		return #cannot produce materials
	if(MaterialTime > 0):
		MaterialTime -= deltaTime
		if(MaterialTime <= 0): produceMaterials() #do something
		return
		
func grow():
	pass
	
func produceMaterials():
	pass
