extends Node2D


var PlantPool = []

func TimeUpdate():
	PlantPool[randi()%PlantPool.size()].randomUpdate()
	

