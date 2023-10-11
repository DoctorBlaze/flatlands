extends "res://entity/Entity.gd"

#after using dash, player gets 4x faster for a short period of time
var dash_boost = 1
var dash_cooldown = 0
var hunger = 100
var coins = 0

var selected_plant = null
var openedPlantMenu = false

var selected_interactable = null
var paper = 2 #paper is being used to learn the samples. every sample needs 1 paper to be learnt. You can find paper in chests or buy from some NPC
var velocity = Vector2()

func _ready():
	entity_type = EntityType.Player
	health = 75
	speed = 512
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
	if velocity != Vector2(0,0): close_uis()
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
	if((player_dir-(position/32)).length() > 5) or (openedPlantMenu): 
		$AskSymbol.visible = false
		selected_plant = null
		return
	var res = get_parent().get_plant_at(player_dir.x,player_dir.y)
	return
	"""
	if(res != null):
		$AskSymbol.visible = true
		$AskSymbol.position = get_local_mouse_position()
		if(PlantsList.p_list.has(res)): selected_plant = PlantsList.p_list[res]
	else:
		$AskSymbol.visible = false
		selected_plant = null
	
	if selected_plant == null:
		$UInode/playerUI/PlantCheckMenu.visible = false
		openedPlantMenu = false
	"""


func _unhandled_input(event):
	if(event is InputEventMouseMotion) or (velocity != Vector2(0,0)):
		any_input_changes()
		
	if Input.is_action_just_pressed("get_plant_info") and selected_plant != null:
		openedPlantMenu = true
		if !samples.has(selected_plant["name"]) and !learned_plants.has(selected_plant["name"]): 
			samples.push_back(selected_plant["name"])
		$UInode/playerUI/PlantCheckMenu.open_ui(self,selected_plant)
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


# uis --------------------------------------------------------------------------

func close_uis():
	if openedPlantMenu:
		$UInode/playerUI/PlantCheckMenu.visible = false
		openedPlantMenu = false
	if $UInode/playerUI/ResearchTable.visible: 
		$UInode/playerUI/ResearchTable.close_ui()


# research and contained items -------------------------------------------------
#after learning, plant will be added to learned_plants and removed from samples
var samples = []
var learned_plants = []

#materials[0] are material names. materials[1] are ammounts of materials
var materials = {}

#learn plant
func make_research(ind):
	if paper <= 0: return false
	if(!learned_plants.has(samples[ind])): learned_plants.push_back(samples[ind])
	samples.remove(ind)
	paper -= 1
	return true

func collect_material(sel_plant):
	if !sel_plant.has("material"): return
	if(!materials.has(sel_plant["material"])): 
		materials.merge({sel_plant["material"]:0},false)
	materials[sel_plant["material"]] += 1
	print(materials)
	return

