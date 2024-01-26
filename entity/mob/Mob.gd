extends "res://entity/Entity.gd"

enum MobState{lookForTarget,interactTarget}
enum MobWalkState {idle,walkTowards, walkAround, walkAway}

var targetNode = null
var targetPosition = null
var mobState = MobState.lookForTarget
var mobWalkState = MobWalkState.idle
var allowablePosDistance = 10
var attackCooldown = 0

var possibleTargets = ["MainCharacter"]


func _ready():
	speed = 200
	entity_type = EntityType.Mob

func _physics_process(delta):
	if(targetPosition != null) and mobWalkState == MobWalkState.walkTowards:
		#$SlimeSprite.play("move")
		var velocity = Vector2(targetPosition - position).normalized() * speed
		set_velocity(velocity)
		move_and_slide()
		if(position.distance_to(targetPosition) < allowablePosDistance):
			mobWalkState = MobWalkState.idle
	elif(targetPosition != null) and mobWalkState == MobWalkState.walkAround:
		#$SlimeSprite.play("move")
		var velocity = Vector2(targetPosition - position).normalized() * speed * 0.5
		set_velocity(velocity)
		move_and_slide()
		if(position.distance_to(targetPosition) < allowablePosDistance):
			mobWalkState = MobWalkState.idle
	else:
		pass
		#$SlimeSprite.play("default")

func _on_SearchArea_body_entered(body):
	if(mobState == MobState.lookForTarget):
		if(body.name == "MainCharacter"):
			targetNode = body
			mobState = MobState.interactTarget
			print("found target")
			_UpdateAI()

func GetPosAround(pos, radius):
	return pos + Vector2(randf()-0.5,randf()-0.5).normalized()*radius
	

#---------------------------------------------------------------------------------------------------



func _UpdateAI():
	UpdateAI()
			
func UpdateAI():
	pass
		
