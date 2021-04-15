using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAbilities", menuName = "AbilitySystem/Abilites/FireAbilities")]
public class Fireabilities : Abilities
{
    
    //[SerializeField]public List<SpellEffects> SPE; Commented Since every ability will have some kind of effect.
    //How far the player will be sent
    [SerializeField] private float knockback;
    //How much % the Player will recieve
    [SerializeField] private float dot;
    //How much the damage the player will take
    [SerializeField] private float damage;
    [SerializeField] private string status;

    //These are getters
    public float KnockBack => knockback;
    public float DOT => dot;
    public float Damage => damage;

    public string Status => status;

    virtual public void Execute(PlayerMovement User, PlayerMovement player){

        foreach (SpellEffects effect in SPE)
        {
            //effect.ExceuteEffect(this, User, player);
        }
    }

}

