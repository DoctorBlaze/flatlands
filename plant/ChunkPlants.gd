extends Node2D

var ParentWorld
var PlantPool = []

func TimeUpdate():
	PlantPool[randi()%PlantPool.size()].randomUpdate()
	

