using Damaging;
using Godot;
using Entities;
//using invSys;

namespace Damaging{

public struct DamageConfig{
    public Entity target;
    public Entity owner;
    //public Weapon weapon;
    public Vector3 damagePosition;
    public float resultDamage;


}

public struct DamageResult{
    public float resultDamage;
    public Entity target;
    public Vector2 damagePosition; 

    public bool targetDied;
}

}