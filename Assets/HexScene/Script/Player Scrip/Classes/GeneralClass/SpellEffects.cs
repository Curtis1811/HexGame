using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellEffectType
{
    Status,
    Wall,
    Projectile,
    AOE,
    DOT
    
}

public abstract class SpellEffects : ScriptableObject
{
    
    // Start is called before the first frame update
    public abstract void ExceuteEffect(Fireabilities ability, GameObject go, GameObject PlayerPrefab);
   
}
