extends "res://entity/Entity.gd"

enum MobState{lookForTarget,interactTarget}
enum MobWalkState {idle,walkTowards, walkAround, walkAway}

var targetNode = null
var targetPosition = null
var mobState = MobState.lookForTarget
var mobWalkState = MobWalkState.idle
var allowablePosDistance = 10


var possibleTargets = ["MainCharacter"]


func _ready():
	speed = 200
	entity_type = EntityType.Mob

func _physics_process(delta):
	if(targetPosition != null) and mobWalkState == MobWalkState.walkTowards:
		$SlimeSprite.play("move")
		var velocity = Vector2(targetPosition - position).normalized() * speed
		move_and_slide(velocity)
		if(position.distance_to(targetPosition) < allowablePosDistance):
			mobWalkState = MobWalkState.idle
	elif(targetPosition != null) and mobWalkState == MobWalkState.walkAround:
		$SlimeSprite.play("move")
		var velocity = Vector2(targetPosition - position).normalized() * speed * 0.64
		move_and_slide(velocity)
		if(position.distance_to(targetPosition) < allowablePosDistance):
			mobWalkState = MobWalkState.idle
	else:
		$SlimeSprite.play("default")

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
	if(mobState == MobState.lookForTarget):
		$Timer.wait_time = randi()%5+1
		mobWalkState = MobWalkState.walkAround
		targetPosition = GetPosAround(position,80)
	if(mobState == MobState.interactTarget) and position.distance_to(targetNode.position) > 10:
		$Timer.wait_time = randf()/2+0.2
		mobWalkState = MobWalkState.walkTowards
		targetPosition = GetPosAround(targetNode.position,50)
