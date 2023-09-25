extends "res://entity/Entity.gd"


enum Mob_target_state {walk_towards, walk_around, walk_away}




func _ready():
	entity_type = EntityType.Mob
