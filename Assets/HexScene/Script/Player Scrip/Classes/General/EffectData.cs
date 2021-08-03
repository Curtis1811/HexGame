using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData
{
    public delegate void EffectStateHandler(PlayerMovement pm, Abilities abilities, float Ammount, bool StateTrue);
    public delegate void UpdateEventHandler(PlayerMovement pm, Abilities abilities, float Deltatime);
    public delegate void ApplyDamageAndKnockBack(PlayerMovement user, Vector3 ProjectilePos, float KnockBack, float Damage);


    //This is called when an effect begins
    public EffectStateHandler onEffectBegin;
    //These is called when an effect ends
    public EffectStateHandler onEffectEnd; 
    //This should be used for things like stuns or damage over time
    public UpdateEventHandler onEffectUpdate;
    //This is to parse the correct amount of damage to a specific place
    public ApplyDamageAndKnockBack onApplyDamageAndKnockBack;



}
