extends "res://entity/Entity.gd"

#after using dash, player gets 4x faster for a short period of time
var dash_boost = 1
var dash_cooldown = 0
var hunger = 100
var selected_plant = null
var samples = []


func _ready():
	health = 75
	print(get_parent().name)

func _physics_process(delta):
	#movement _____________________________________________________________
	var velocity = Vector2()
		
	if Input.is_action_pressed("go_left"):
		velocity.x = -1
		
	elif Input.is_action_pressed("go_right"):
		velocity.x = 1
		
	if Input.is_action_pressed("go_down"):
		velocity.y = 1
		
	elif Input.is_action_pressed("go_up"):
		velocity.y = -1

	#dashes _____________________________________________________________
	if Input.is_action_just_pressed("dash") and dash_cooldown <= 0:
		$dashSprite.play("dash")
		dash_boost = 4
		dash_cooldown = 4
	
	velocity = velocity.normalized()*speed
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
		
func _process(delta):
	$UInode/playerUI/PlayerOverlay/HpBar.value = health
	$UInode/playerUI/PlayerOverlay/HungerLabel.text = str(hunger)
	
	var player_dir = get_global_mouse_position()/32
	if((player_dir-(position/32)).length() > 5): 
		$AskSymbol.visible = false
		selected_plant = null
		return
	var final_loc = player_dir 
	var res = get_parent().get_plant_at(final_loc.x,final_loc.y)
	if(res != null):
		$AskSymbol.visible = true
		$AskSymbol.position = get_local_mouse_position()
		selected_plant = res
	else:
		$AskSymbol.visible = false
		selected_plant = null
	
	if selected_plant == null:
		$UInode/playerUI/PlantCheckMenu.visible = false


func _unhandled_input(event):
	if Input.is_action_just_pressed("get_plant_info") and selected_plant != null:
		$UInode/playerUI/PlantCheckMenu.visible = true
		$UInode/playerUI/PlantCheckMenu/Book/PlantName.text = selected_plant.get("name")
		$UInode/playerUI/PlantCheckMenu/Book/OterInfo.text = selected_plant.get("short_desc")
		
		if(samples.has(selected_plant.get("name"))):
			$UInode/playerUI/PlantCheckMenu/Book/SampleLabel.text = "You alredy collected this sample."
		else:
			$UInode/playerUI/PlantCheckMenu/Book/SampleLabel.text = "You collected this sample. You can learn it on the research table."
			samples.push_back(selected_plant.get("name"))
			print(samples)
		#print(selected_plant.get("short_descS



