using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterAbilities", menuName = "AbilitySystem/Abilites/WaterAbilities")]
public class WaterAbilities : Abilities
{
    //[SerializeField] public List<SpellEffects> SPE;
    //How far the player will be sent
    [SerializeField] private float knockback;
    //How much % the Player will recieve
    [SerializeField] private float dot;
    //How much the damage the player will take
    [SerializeField] private float damage;
    //This will be the status effect that the Ability Does.
    [SerializeField] private string status;

    //These are getters
    public float KnockBack => knockback;
    public float DOT => dot;
    public float Damage => damage;
    public string Status => status;

    virtual public void Execute(PlayerMovement go, PlayerMovement player)
    {
        foreach (SpellEffects effect in SPE)    
        {
            //effect.ExceuteEffect(this, go, player);
        }
    }
}
