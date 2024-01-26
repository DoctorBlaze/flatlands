using Godot;
using Damaging;



namespace Statistycs{

/// <summary>
/// list of all possible modifiers. As for example, maxHealth is one of stats (current health or experience values are not included)
/// </summary>
public enum StatList{
    
    //hp and regen -----------
    maxHealth,
    regeneration,

    //movement -
    walkSpeed,
    jumpStrength,




    //damage ---

    /// <summary>
    /// generic modifier, that will be added or multiplied to damage of any type
    /// </summary>
    damage,

    projectileDamage,
    projectilePierce,
    projectileSpeed,

    /// <summary>
    /// works only with projectile damage
    /// </summary>
    critDamage,
    critChance,

    explosionDamage,
    explosionRadius,
    explosionKnockback,




    //protection -----------

    /// <summary>
    /// if projectile pierces the target, it deals more damage; 
    /// if (bullet piercing < armor, armor won't be pierced and target recieve only n..m% of damage
    /// </summary>
    armor,

    /// <summary>
    /// ANY type of damage will be divided by this resistance
    /// </summary>
    resistance,

    /// <summary>
    /// projectile damage will be divided by this resistance
    /// </summary>
    projectileResistance,

    /// <summary>
    /// explosion damage will be divided by this resistance
    /// </summary>
    explosionResistance,

    /// <summary>
    /// thermal damage will be divided by this resistance
    /// </summary>
    thermalResistance,

    

    
}

}