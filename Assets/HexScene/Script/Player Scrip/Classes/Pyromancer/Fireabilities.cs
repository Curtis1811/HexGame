using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAbilities", menuName = "AbilitySystem/Abilites/FireAbilities")]
public class Fireabilities : Abilities
{
    
    List<SpellEffects> effects;
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

    virtual public void Execute(GameObject go, GameObject player){

        foreach (SpellEffects effect in effects)
        {
            effect.ExceuteEffect(this,go,player);
        }
    }

}

