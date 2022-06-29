using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Here we need to a way to referenace the Effect Data
[CreateAssetMenu(fileName = "FlatDamageEffect", menuName = "EffectSystem/Effects/FlatDamageEffect")]
public class FlatDamageEffect : SpellEffects
{
    public override event EffectEvent onEffect;
    SpellEffectType type = SpellEffectType.FlatDamage;

}
