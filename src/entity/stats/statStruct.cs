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
    public bool addMode;
    public StatList stat;


    public Modifier(StatList stat_, bool addMode_){
        stat = stat_;
        addMode = addMode_;

        if(addMode) value = 0;
        else value = 1;
    }

    public Modifier(float value_ ,StatList stat_, bool addMode_){
        stat = stat_;
        addMode = addMode_;
        value = value_;
    }
}



}