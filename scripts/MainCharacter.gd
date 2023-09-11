extends "res://scripts/Entity.gd"

func _physics_process(delta):

	var velocity = Vector2()
	
	if Input.is_action_pressed("go_down"):
		velocity.y = speed
		$AnimatedSprite.play("Go Down")
		
	elif Input.is_action_pressed("go_up"):
		velocity.y = -speed
		$AnimatedSprite.play("Go Up")
		
	elif Input.is_action_pressed("go_left"):
		velocity.x = -speed
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = true
		
	elif Input.is_action_pressed("go_right"):
		velocity.x = speed
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = false

	move_and_slide(velocity)
