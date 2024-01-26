

using System.Collections.Generic;
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
    }

    public void SpawnPlayer(Player p){
        if(players.Contains(p)) return;
        entities.Add(p);
        players.Add(p);
        world.AddChild(p);
    }


    public void DestroyEntity(Entity e){
        if(entities.Contains(e)){
            entities.Remove(e);
        }
        e.QueueFree();
    }





}


}