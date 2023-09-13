extends "res://entity/Entity.gd"

#after using dash, player gets 4x faster for a short period of time
var dash_boost = 1
var dash_cooldown = 0

func _physics_process(delta):
	#movement _____________________________________________________________
	var velocity = Vector2()
		
	if Input.is_action_pressed("go_left"):
		velocity.x = -speed
		
	elif Input.is_action_pressed("go_right"):
		velocity.x = speed
		
	if Input.is_action_pressed("go_down"):
		velocity.y = speed
		
	elif Input.is_action_pressed("go_up"):
		velocity.y = -speed

	#dashes _____________________________________________________________
	if Input.is_action_just_pressed("dash") and dash_cooldown <= 0:
		$dashSprite.play("dash")
		dash_boost = 4
		dash_cooldown = 4
	
	#decrease dash boost when time passes
	if dash_boost > 1:
		velocity = velocity * dash_boost
		dash_boost -= delta*8
	else:
		$dashSprite.play("none")
	
	if dash_cooldown > 0:
		dash_cooldown -= delta
	
	#final speed _____________________________________________________________
	init_animations(velocity)
	move_and_slide(velocity)
	
	
func init_animations(velocity):
	YSort 
	if velocity.y > 0:
		$AnimatedSprite.play("Go Down")
		return
	elif velocity.y < 0:
		$AnimatedSprite.play("Go Up")
		return
			
	elif velocity.x > 0:
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = false
		return
	elif velocity.x < 0:
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = true
		return
	else: 
		$AnimatedSprite.play("Idle")
