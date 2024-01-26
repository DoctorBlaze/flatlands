using Godot;


namespace Statistycs{

public struct AttackStatsBunch{
    public float damage;
    public float projectileDamage;
    public float projectilePierce;
    public float projectileSpeed;
    public float critDamage;
    public float critChance;
    public float explosionDamage;
    public float explosionRadius;
    public float explosionKnockback;


    public static AttackStatsBunch operator +(AttackStatsBunch a,AttackStatsBunch b){
        return new AttackStatsBunch(){
        damage = a.damage + b.damage,
        projectileDamage = a.projectileDamage + b.projectileDamage,
        projectilePierce = a.projectilePierce + b.projectilePierce,
        projectileSpeed = a.projectileSpeed + b.projectileSpeed,
        critDamage = a.critDamage + b.critDamage,
        critChance = a.critChance + b.critChance,
        explosionDamage = a.explosionDamage + b.explosionDamage,
        explosionRadius = a.explosionRadius + b.explosionRadius,
        explosionKnockback = a.explosionKnockback + b.explosionKnockback,
        };
    }

    public static AttackStatsBunch operator *(AttackStatsBunch a,AttackStatsBunch b){
        return new AttackStatsBunch(){
        damage = a.damage * b.damage,
        projectileDamage = a.projectileDamage * b.projectileDamage,
        projectilePierce = a.projectilePierce * b.projectilePierce,
        projectileSpeed = a.projectileSpeed * b.projectileSpeed,
        critDamage = a.critDamage * b.critDamage,
        critChance = a.critChance * b.critChance,
        explosionDamage = a.explosionDamage * b.explosionDamage,
        explosionRadius = a.explosionRadius * b.explosionRadius,
        explosionKnockback = a.explosionKnockback * b.explosionKnockback,
        };
    }

    public void AddModifier(Modifier modifier){
        if(modifier.addMode == true){
            switch(modifier.stat){
                case(StatList.damage): damage += modifier.value; break;
                case(StatList.projectileDamage): projectileDamage += modifier.value; break;
                case(StatList.projectilePierce): projectilePierce += modifier.value; break;
                case(StatList.projectileSpeed): projectileSpeed += modifier.value; break;
                case(StatList.critDamage): critDamage += modifier.value; break;
                case(StatList.critChance): critChance += modifier.value; break;
                case(StatList.explosionDamage): explosionDamage += modifier.value; break;
                case(StatList.explosionRadius): explosionRadius += modifier.value; break;
                case(StatList.explosionKnockback): explosionKnockback += modifier.value; break;
                default: return;
            }
        }  
        else{
            switch(modifier.stat){
                case(StatList.damage): damage *= modifier.value; break;
                case(StatList.projectileDamage): projectileDamage *= modifier.value; break;
                case(StatList.projectilePierce): projectilePierce *= modifier.value; break;
                case(StatList.projectileSpeed): projectileSpeed *= modifier.value; break;
                case(StatList.critDamage): critDamage *= modifier.value; break;
                case(StatList.critChance): critChance *= modifier.value; break;
                case(StatList.explosionDamage): explosionDamage *= modifier.value; break;
                case(StatList.explosionRadius): explosionRadius *= modifier.value; break;
                case(StatList.explosionKnockback): explosionKnockback *= modifier.value; break;
                default: return;
            }
        }
    }
}


}