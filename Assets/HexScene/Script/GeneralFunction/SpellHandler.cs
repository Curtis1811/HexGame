using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public static class SpellHandler 
{
    // IN HERE WE COULD MOST LIKLETY RETURN WHAT WE WANT SO WE DONT HAVE TO HANDEL EVREYTHING IN THE PLAYER SCIRPT


    // All these need to do is hold the effects that will occure.
    public static void OnEffect_Stun(PlayerMovement User, Abilities abilityUsed, float Amount, bool isStunned)
    {
        User.setStun(isStunned, Amount);
        Debug.Log("StunThisDude");
    }

    public static void OnEffect_Heal(PlayerMovement user, Abilities abilityUsed)
    {
        Debug.Log("We are setting the Health here");
        user.health += 10;
    }
    
    public static void OnDragEffect(GameObject gameObject, PlayerMovement user, PlayerMovement PlayerToDrag)
    {
        //This will drag the player toward the centre of the GameObject
    }

    public static void OnSpeedUp(PlayerMovement user, Abilities abilities, float Amount, bool stateTrue)
    {
        //user.setSpeed(Amount);
        user.CmdChangeSpeed(Amount);
        Debug.Log("We are setting the speed here");
        //user.scaler = Ammount;
    }

    //The use of Scale Will need to be a synced Variable
    public static void OnSpeedUpOff(PlayerMovement user, Abilities abilities, float Amount, bool stateTrue)
    {
        user.setSpeed(Amount);
        user.scaler = Amount;
    }

    public static void OnDamageOverTime(PlayerMovement user, Abilities abilities, float DamageAmount, bool stateTrue)
    {
        Debug.Log("Damage Over Time");
        user.health -= DamageAmount;
    }

    public static void OnFlatDamage(PlayerMovement user, float DamageAmount)
    {
        Debug.Log("Damage");
        user.ApplyDamage(DamageAmount);
        
    }

    public static void OnApplyKnockBack(PlayerMovement user, Vector3 ProjectilePos, float KnockBackAmount , float Damage)
    {
        Debug.Log("ApplyKnockBack");
        user.ApplyKnockBack(KnockBackAmount, ProjectilePos);
        OnFlatDamage(user, Damage);
    }
    //Here is where we are going to try and define some event handlers to handle play stuff
}
