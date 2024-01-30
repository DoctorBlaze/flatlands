using Godot;


namespace Statistycs{

public struct StatsBunch{
    public float maxHealth;
    public float regeneration;
    public float walkSpeed;
    public float jumpStrength;
    public float damage;
    public float projectileDamage;
    public float projectilePierce;
    public float projectileSpeed;
    public float critDamage;
    public float critChance;
    public float explosionDamage;
    public float explosionRadius;
    public float explosionKnockback;
    public float armor;
    public float resistance;
    public float projectileResistance;
    public float explosionResistance;
    public float thermalResistance;


    public static StatsBunch operator +(StatsBunch a,StatsBunch b){
        return new StatsBunch(){
        maxHealth = a.maxHealth + b.maxHealth,
        regeneration = a.regeneration + b.regeneration,
        walkSpeed = a.walkSpeed + b.walkSpeed,
        jumpStrength = a.jumpStrength + b.jumpStrength,
        damage = a.damage + b.damage,
        projectileDamage = a.projectileDamage + b.projectileDamage,
        projectilePierce = a.projectilePierce + b.projectilePierce,
        projectileSpeed = a.projectileSpeed + b.projectileSpeed,
        critDamage = a.critDamage + b.critDamage,
        critChance = a.critChance + b.critChance,
        explosionDamage = a.explosionDamage + b.explosionDamage,
        explosionRadius = a.explosionRadius + b.explosionRadius,
        explosionKnockback = a.explosionKnockback + b.explosionKnockback,
        armor = a.armor + b.armor,
        resistance = a.resistance + b.resistance,
        projectileResistance = a.projectileResistance + b.projectileResistance,
        explosionResistance = a.explosionResistance + b.explosionResistance,
        thermalResistance = a.thermalResistance + b.thermalResistance,
        };
    }

    public static StatsBunch operator *(StatsBunch a,StatsBunch b){
        return new StatsBunch(){
        maxHealth = a.maxHealth * b.maxHealth,
        regeneration = a.regeneration * b.regeneration,
        walkSpeed = a.walkSpeed * b.walkSpeed,
        jumpStrength = a.jumpStrength * b.jumpStrength,
        damage = a.damage * b.damage,
        projectileDamage = a.projectileDamage * b.projectileDamage,
        projectilePierce = a.projectilePierce * b.projectilePierce,
        projectileSpeed = a.projectileSpeed * b.projectileSpeed,
        critDamage = a.critDamage * b.critDamage,
        critChance = a.critChance * b.critChance,
        explosionDamage = a.explosionDamage * b.explosionDamage,
        explosionRadius = a.explosionRadius * b.explosionRadius,
        explosionKnockback = a.explosionKnockback * b.explosionKnockback,
        armor = a.armor * b.armor,
        resistance = a.resistance * b.resistance,
        projectileResistance = a.projectileResistance * b.projectileResistance,
        explosionResistance = a.explosionResistance * b.explosionResistance,
        thermalResistance = a.thermalResistance * b.thermalResistance,
        };
    }

    public void AddModifier(Modifier modifier){
        if(modifier.type == ModifierType.Add){
            switch(modifier.stat){
                case(StatList.maxHealth): maxHealth += modifier.value; break;
                case(StatList.regeneration): regeneration += modifier.value; break;
                case(StatList.walkSpeed): walkSpeed += modifier.value; break;
                case(StatList.jumpStrength): jumpStrength += modifier.value; break;
                case(StatList.damage): damage += modifier.value; break;
                case(StatList.projectileDamage): projectileDamage += modifier.value; break;
                case(StatList.projectilePierce): projectilePierce += modifier.value; break;
                case(StatList.projectileSpeed): projectileSpeed += modifier.value; break;
                case(StatList.critDamage): critDamage += modifier.value; break;
                case(StatList.critChance): critChance += modifier.value; break;
                case(StatList.explosionDamage): explosionDamage += modifier.value; break;
                case(StatList.explosionRadius): explosionRadius += modifier.value; break;
                case(StatList.explosionKnockback): explosionKnockback += modifier.value; break;
                case(StatList.armor): armor += modifier.value; break;
                case(StatList.resistance): resistance += modifier.value; break;
                case(StatList.projectileResistance): projectileResistance += modifier.value; break;
                case(StatList.explosionResistance): explosionResistance += modifier.value; break;
                case(StatList.thermalResistance): thermalResistance += modifier.value; break;
                default: return;
            }
        }  
        else{
            switch(modifier.stat){
                case(StatList.maxHealth): maxHealth *= modifier.value; break;
                case(StatList.regeneration): regeneration *= modifier.value; break;
                case(StatList.walkSpeed): walkSpeed *= modifier.value; break;
                case(StatList.jumpStrength): jumpStrength *= modifier.value; break;
                case(StatList.damage): damage *= modifier.value; break;
                case(StatList.projectileDamage): projectileDamage *= modifier.value; break;
                case(StatList.projectilePierce): projectilePierce *= modifier.value; break;
                case(StatList.projectileSpeed): projectileSpeed *= modifier.value; break;
                case(StatList.critDamage): critDamage *= modifier.value; break;
                case(StatList.critChance): critChance *= modifier.value; break;
                case(StatList.explosionDamage): explosionDamage *= modifier.value; break;
                case(StatList.explosionRadius): explosionRadius *= modifier.value; break;
                case(StatList.explosionKnockback): explosionKnockback *= modifier.value; break;
                case(StatList.armor): armor *= modifier.value; break;
                case(StatList.resistance): resistance *= modifier.value; break;
                case(StatList.projectileResistance): projectileResistance *= modifier.value; break;
                case(StatList.explosionResistance): explosionResistance *= modifier.value; break;
                case(StatList.thermalResistance): thermalResistance *= modifier.value; break;
                default: return;
            }
        }
    }

    public void AddModifiers(Modifier [] modifiers){
        foreach(Modifier mod in modifiers){
            AddModifier(mod);
        }
    }

}


}