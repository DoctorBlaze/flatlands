extends CharacterBody2D

enum EntityType {Mob,Player,NPC,Ambient}
var entity_type

var health = 100

var armor = 0
var resistance = 1

var speed = 180 
var hurt_cooldown = 0


func _process(delta):
	if(hurt_cooldown > 0): hurt_cooldown -= delta
	


# health and damage ------------------------------------------------------------
func recieve_damage(dmg):
	if(hurt_cooldown > 0): return
	hurt_cooldown = 0.5
	
	var res_dmg = 0
	if(dmg > armor):
		res_dmg = dmg - armor/100
	else:
		res_dmg = dmg - armor/50
		
	if(dmg < 0): return
	health -= (dmg / resistance)

