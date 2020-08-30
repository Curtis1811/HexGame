using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu (fileName = "FireAbilities" , menuName ="AbilitySystem/Abilites/FireAbilities")]
public class Fireabilities : Abilities
{
    public void Awake()
    {
        type = abilityType.Fire;

    }
    //How far the player will be sent
    [SerializeField] private float kockback;
    //How much % the Player will recieve
    [SerializeField] private float dot;
    //How much the damage the player will take
    [SerializeField] private float damage;

    //These are getters
    public float Kockback => kockback;
    public float DOT => dot;
    public float Damange => damage;

    virtual public void Execute() {

    }

}

