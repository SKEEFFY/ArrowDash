using UnityEngine;

public class ChangeSpriteEvent 
{
    public readonly Material NewMaterial;

    public ChangeSpriteEvent(Material newMaterial)
    {
        NewMaterial = newMaterial;
    }
}
