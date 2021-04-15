using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Here we need to a way to referenace the Effect Data
[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/StatusEffects")]
public class Effects : SpellEffects
{
    public override event EffectEvent onEffect;
    SpellEffectType type = SpellEffectType.Status;

    public enum StatusType
    {
        stunned,
        slowed,
        speedIncrease
    }

    /*

    // Start is called before the first frame update
    public override void ExceuteEffect(Abilities skillThatsUsed, PlayerMovement PlayerThatAttacked, PlayerMovement PlayTakingAttack)// We will also need to parse in the status effect.
    {
        onEffect?.Invoke(PlayerThatAttacked, skillThatsUsed);

        //Here is where we will invoke the onEffect I think.
        Debug.Log("This is a Status SpellEffect");
    }*/

    
}


/*
[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Wall")]
public class WallEffects : SpellEffects
{
    SpellEffectType type = SpellEffectType.Wall;
    public override event EffectEvent onEffect;
    public override void ExceuteEffect(Abilities skillThatsUsed, PlayerMovement PlayerThatAttacked, PlayerMovement PlayTakingAttack)
    {
        Debug.Log("This is a wall effect");

    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Projectile")]
public class ProjectileEffects : SpellEffects
{
    public override event EffectEvent onEffect;
    SpellEffectType type = SpellEffectType.Projectile;
    public override void ExceuteEffect(Abilities skillThatsUsed, PlayerMovement PlayerThatAttacked, PlayerMovement PlayTakingAttack)
    {
        //Instantiate<GameObject>(go,playerPrefab.transform.position,Quaternion.identity);
        Debug.Log("This is a projectile Effect");
       //throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/AOE")]
public class AOE : SpellEffects {
    SpellEffectType type = SpellEffectType.AOE;
    public override event EffectEvent onEffect;
    public override void ExceuteEffect(Abilities skillThatsUsed, PlayerMovement PlayerThatAttacked, PlayerMovement PlayTakingAttack)
    {
        Debug.Log("This is an AOE effect");
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/DOT")]
public class DOTEffects : SpellEffects {
    SpellEffectType type = SpellEffectType.DOT;
    public override event EffectEvent onEffect;
    public override void ExceuteEffect(Abilities skillThatsUsed, PlayerMovement PlayerThatAttacked, PlayerMovement PlayTakingAttack)
    {

        Debug.Log("This is a DOT effect");
    }
}*/
