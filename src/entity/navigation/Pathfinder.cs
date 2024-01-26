
using Godot;

namespace Entities{

/// <summary>
/// pathfinder class
/// </summary>
public class PFind{

    /// <summary>
    /// seach if somethisng's blocking the way
    /// </summary>
    private RayCast3D solidRaycast;


    public Entity entity;


    /// <summary>
    /// Position in the world to walk to
    /// </summary>
    public Vector3 dest;

    /// <summary>
    /// if difference between current posiotion is higher that this value, give up walking.
    /// </summary>
    public float maxDestDistance = 200;


    /// <summary>
    /// how close to exact destination Entity has to be to stop
    /// </summary>
    public float destPrecision = 4;

    public PFind(){
        solidRaycast = new RayCast3D();
    }

    


}


public enum Checkstate{
    free,
    fLeft,
    fRight,
    fTop,
    fBottom,
    blocked
}

public struct TraceResult{
    public bool blocked;
    public bool entityFound;
    public Vector3 hitPosition;


}



}