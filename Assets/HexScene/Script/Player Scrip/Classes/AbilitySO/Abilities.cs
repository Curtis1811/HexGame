using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum abilityType{
    Fire,
    Water,
    Air,
    Earth
}

public abstract class Abilities : ScriptableObject , ICooldownInterface
{

    [Header("UI")]
    // This is the name of the ability
    [SerializeField] new private string name;
    // This is the sprite of the Ability icons
    [SerializeField] private Sprite icon;
    

    [Header("Types")]
    [SerializeField] public abilityType type;
    //[SerializeField] public SpellEffects SPE;
    //[SerializeField] public List<SpellEffects> SPE;

    [Header("Discription")]
    [TextArea(15, 20)]
    [SerializeField] private string discription;
    [SerializeField] private string prefabName; // Maybe Obsolete
    [SerializeField] public bool isDefaultAbility;

    [Header("Spell Variables")]
    [SerializeField] private float coolDown;
    [SerializeField] private float duration;
    [SerializeField] public List<SpellEffects> SPE;
    [SerializeField] public int SpellId = 1;
    
    //We will need some kind of Effect Visual Object

    //These are getters for the Class
    public string Name => name;
    public Sprite Icon => icon;
    public string Discription => discription;
    public string PrefabName => prefabName;
    public float CoolDown => coolDown;
    public float Duration => duration;
    public int id => SpellId;
    public float CooldownDuration => coolDown;
}