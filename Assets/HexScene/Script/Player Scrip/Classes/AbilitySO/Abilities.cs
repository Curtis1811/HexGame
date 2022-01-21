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
public enum SpawnType{
    SpawnOnSelf,
    SpawnOnTargetPoint,
    SpawnOnCastPoint
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
    [SerializeField] public SpawnType SpawnType;

    [Header("Discription")]
    [TextArea(15, 20)]
    [SerializeField] private string discription;
    [SerializeField] private string prefabName; //Maybe Obsolete
    [SerializeField] public bool isDefaultAbility;

    [Header("Spell Variables")]
    //This value will deal with the cooldown of the spells
    [SerializeField] private float coolDown;
    //This deals with the duration of the spell.
    [SerializeField] private float duration;
    //This will deal with the any value that the effect will have, From dmg to amount of a shield.
    [SerializeField] public float Value;
    [SerializeField] public List<SpellEffects> SPE;
    [SerializeField] public int SpellId = 0;
    [SerializeField] public bool Charge = false;
    [SerializeField] GameObject Prefab;
    //We will need some kind of Effect Visual Object
    //[HideInInspector]
    public Vector3 spawnPoint{get;set;}
    public Quaternion objectRotation{get;set;}

    //These are getters for the Class
    public string Name => name;
    public Sprite Icon => icon;
    public string Discription => discription;
    public string PrefabName => prefabName;
    public float CoolDown => coolDown;
    public float Duration => duration;
    public int id => SpellId;
    public float CooldownDuration => coolDown;
    public bool isCharge => Charge;
    public GameObject GameObjectPrefab => Prefab;
    public SpawnType spawnType => SpawnType;

    //We may need to create some setters.
    public virtual void ExecuteEvent(){
        //This will be used to 
        Debug.Log("Here we are executing the Ability");
    }

}