extends "res://entity/Entity.gd"

#after using dash, player gets 4x faster for a short period of time
var dash_boost = 1
var dash_cooldown = 0
var hunger = 100
var coins = 0
var selected_plant = null
var selected_interactable = null
var paper = 2 #paper is being used to learn the samples. every sample needs 1 paper to be learnt. You can find paper in chests or buy from some NPC
var velocity = Vector2()

func _ready():
	health = 75
	print(get_parent().name)

func _physics_process(delta):
	#movement _____________________________________________________________
	velocity = Vector2()
		
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
	if not global.is_paused:
		move_and_slide(velocity)
	
	
func init_animations(velocity):
	YSort 
	if velocity.y > 0 and not global.is_paused:
		$AnimatedSprite.play("Go Down")
		return
	elif velocity.y < 0 and not global.is_paused:
		$AnimatedSprite.play("Go Up")
		return
			
	elif velocity.x > 0 and not global.is_paused:
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = false
		return
	elif velocity.x < 0 and not global.is_paused:
		$AnimatedSprite.play("Go Side")
		$AnimatedSprite.flip_h = true
		return
	else: 
		$AnimatedSprite.play("Idle")
		
func _process(delta):
	$UInode/playerUI/PlayerOverlay/HpBar.value = health
	$UInode/playerUI/PlayerOverlay/HungerLabel.text = str(hunger)

func any_input_changes():
	var player_dir = get_global_mouse_position()/32
	if((player_dir-(position/32)).length() > 5): 
		$AskSymbol.visible = false
		selected_plant = null
		return
	var res = get_parent().get_plant_at(player_dir.x,player_dir.y)
	if(res != null):
		$AskSymbol.visible = true
		$AskSymbol.position = get_local_mouse_position()
		if(PlantsList.p_list.has(res)): selected_plant = PlantsList.p_list[res]
	else:
		$AskSymbol.visible = false
		selected_plant = null
	
	if selected_plant == null:
		$UInode/playerUI/PlantCheckMenu.visible = false
		


func _unhandled_input(event):
	if(event is InputEventMouseMotion) or (velocity != Vector2(0,0)):
		any_input_changes()
		
	if Input.is_action_just_pressed("get_plant_info") and selected_plant != null:
		$UInode/playerUI/PlantCheckMenu.visible = true
		$UInode/playerUI/PlantCheckMenu/Book/PlantName.text = selected_plant.get("name")
		$UInode/playerUI/PlantCheckMenu/Book/OterInfo.text = selected_plant.get("short_desc")
		
		if(learned_plants.has(selected_plant.get("name"))):
			$UInode/playerUI/PlantCheckMenu/Book/SampleLabel.text = "You alredy learned this plant! Yay!"
		elif(samples.has(selected_plant.get("name"))):
			$UInode/playerUI/PlantCheckMenu/Book/SampleLabel.text = "You alredy collected this sample. Use research table to learn it."
		else:
			$UInode/playerUI/PlantCheckMenu/Book/SampleLabel.text = "You collected this sample. Now you can learn it on the research table."
			samples.push_back(selected_plant.get("name"))
			print(samples)
			
	if Input.is_action_just_pressed("interact") and selected_interactable != null:
		use_interactable()


# interactables -------------------------------------------------------------------------
#select closest interactable
func _on_InteractArea_area_entered(area):
	var all_inters = $InteractArea.get_overlapping_areas()
	var dst = 1000
	for i in all_inters:
		if(dst > Vector2(global_position - i.global_position).length()):
			dst = Vector2(global_position - i.global_position).length()
			selected_interactable = i
	$UInode/playerUI/InteractHint.visible = true


func _on_InteractArea_area_exited(area):
	if($InteractArea.get_overlapping_areas().size() <= 0): 
		selected_interactable = null
		$UInode/playerUI/InteractHint.visible = false
		
func use_interactable():
	if(selected_interactable.get_name() == "ResearchTableArea"):
		$UInode/playerUI/ResearchTable.open_ui(self,samples)


# research -------------------------------------------------------------------------
var samples = []
var learned_plants = []

func make_research(ind):
	if paper < 1: return false
	if(!learned_plants.has(samples[ind])): learned_plants.push_back(samples[ind])
	samples.remove(ind)
	paper -= 1
	return true
