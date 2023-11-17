extends "res://entity/mob/Mob.gd"

var jumping = true
var defSpeed = 200
var jumpLen = 0.75

func UpdateAI():
	if(mobState == MobState.lookForTarget):
		$Timer.wait_time = randi()%5+1
		mobWalkState = MobWalkState.walkAround
		targetPosition = GetPosAround(position,80)
	if(mobState == MobState.interactTarget):
		if position.distance_to(targetNode.position) > 30:
			$Timer.wait_time = randf()/2+0.2
			mobWalkState = MobWalkState.walkTowards
			targetPosition = GetPosAround(targetNode.position,50)
		else:
			$Timer.wait_time = 0.5
			targetNode.recieve_damage(5)


func JumpInterval():
	if jumping:
		$JumpTimer.wait_time = 0.75
		print("jumppp")
		$SlimeSprite.play("move")
		speed = defSpeed
		jumping = false
	else:
		if(mobState == MobState.interactTarget): $JumpTimer.wait_time = 0.75
		else: $JumpTimer.wait_time = randi()%2 +1
		$SlimeSprite.play("default")
		speed = 0
		jumping = true
		
