using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Maybe here we will need an Enum for Status Effects.

public abstract class SpellEffects : ScriptableObject
{
    public delegate void EffectEvent(PlayerMovement User, Abilities abilityUsed);
    public abstract event EffectEvent onEffect;
    public EffectData effectData = new EffectData();
    public int testInt;
    public enum SpellEffectType
    {
        Status,
        Wall,
        Projectile,
        AOE,
        DOT,
        FlatDamage
    }
    //Here we are defining an effect event that will take in the 2 different playerMovement scripts. For now we shall change the HP to 5 As a test.
    //public abstract void ExceuteEffect(Abilities ability, PlayerMovement go, PlayerMovement PlayerPrefab);
    //public event Action<PlayerMovement,PlayerMovement,float> onEffect_Stunned;
    //public event Action<PlayerMovement,float> onEffect_Heal;
}
