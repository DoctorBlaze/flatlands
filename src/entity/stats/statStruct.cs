using Godot;
using Damaging;
using System;
//using Statistycs;



namespace Statistycs{

/// <summary>
/// single stat modifier
/// for entities/players StatArray is used
/// </summary>
public struct Modifier{

    /// <summary>
    /// value affecting the stats;
    /// negative values will decrease it
    /// </summary>
    public float value;

    /// <summary>
    /// if true, stat will be added;
    /// else it will multiply the result
    /// </summary>
    public ModifierType type;
    public StatList stat;


    public Modifier(StatList stat_, ModifierType type_){
        stat = stat_;
        type = type_;

        if(type == ModifierType.Add) value = 0;
        else value = 1;
    }

    public Modifier(float value_ ,StatList stat_, ModifierType type_){
        stat = stat_;
        type = type_;
        value = value_;
    }
}

public enum ModifierType{
    Add,
    Mul
}

}