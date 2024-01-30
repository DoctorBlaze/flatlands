
using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using Entities;

namespace World{

public class EntityPool{
    /// <summary>
    /// entities (including players)
    /// </summary>
    public List<Entity> entities;

    /// <summary>
    /// only players
    /// </summary>
    public List<Player> players;


    /// <summary>
    /// parent world;
    /// all entities will be added to it
    /// </summary>
    World2D world;
    public EntityPool(World2D w){
        world = w;
        entities = new();
        players = new();
    }

    public void SpawnEntity(Entity e){
        if(entities.Contains(e)) return;
        entities.Add(e);
        world.AddChild(e);
        e.epool = this;
    }

    public void SpawnPlayer(Player p){
        if(players.Contains(p)) return;
        entities.Add(p);
        players.Add(p);
        world.AddChild(p);
        p.epool = this;
    }



    public void DestroyEntity(Entity e){
        if(entities.Contains(e)){
            GD.Print(e+"enity destroyed");
            entities.Remove(e);
            e.QueueFree();
        }
        else GD.PrintErr("no entity in pool");
        
    }



    public void DisableEntity(Entity e){
        e.Visible=false;
        e.SetProcess(false);
        e.SetPhysicsProcess(false);
    }

    public void EnableEntity(Entity e){
        e.Visible=true;
        e.SetProcess(true);
        e.SetPhysicsProcess(true);
    }


    /// <summary>
    /// get nearest entity
    /// </summary>
    public Entity GetNearEntity(Godot.Vector2 pos, float maxRange){
        float range, minRange=0f;
        Entity ret = null;

        foreach(Entity e in entities){
            range = Mathf.Abs((e.Position - pos).Length());
            if(range > 0.01f && range < maxRange && (ret==null || range<minRange)){
                ret = e;
                minRange = range;
            }
        }

        return ret;
    }

    public Entity GetNearEntity(Godot.Vector2 pos, float maxRange, List<Type> types){
        float range, minRange=0f;
        Entity ret = null;

        foreach(Entity e in entities){
            if(types.Contains(e.GetType())){
                range = (e.Position - pos).Length();
                if(range > 0.1f && range < maxRange && (ret==null || range<minRange)){
                    ret = e;
                    minRange = range;
                }
            }
        }

        return ret;
    }

    public Godot.Collections.Array<Entity> SelectEntities(Godot.Vector2 pos, float maxRange){
        float range;
        Godot.Collections.Array<Entity> ret = new();

        foreach(Entity e in entities){
            range = Mathf.Abs((e.Position - pos).Length());
            if(range > 0.01f && range < maxRange){
                ret.Add(e);
            }
        }
        
        return ret;
    }


    public Godot.Collections.Array<Entity> SelectEntities(Godot.Vector2 pos, float maxRange, RotationState rstate){
        float range;
        Godot.Vector2 dir;
        Godot.Collections.Array<Entity> ret = new();

        foreach(Entity e in entities){
            range = Mathf.Abs((e.Position - pos).Length());
            dir = (e.Position - pos).Normalized();
            if(range > 0.01f && range < maxRange){
                if(rstate==RotationState.Front && dir.Y >= 0.5f){
                    ret.Add(e);
                }
                else if(rstate==RotationState.Back && dir.Y <= -0.5f){
                    ret.Add(e);
                }
                else if(rstate==RotationState.Right && dir.X >= 0.5f){
                    ret.Add(e);
                }
                else if(rstate==RotationState.Left && dir.X <= -0.5f){
                    ret.Add(e);
                }
            }
        }

        return ret;
    }




}


}