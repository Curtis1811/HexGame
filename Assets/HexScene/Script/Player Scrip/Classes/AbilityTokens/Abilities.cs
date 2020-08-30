using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum abilityType{
    Fire,
    Water,
    Air
}

public abstract class Abilities : ScriptableObject
{

    // This is the name of the ability
    [SerializeField] new private string name = "New Ability";

    // This is the sprite of the Ability icons
    [SerializeField] private Sprite icon = null;

    // This is to decide if the Ability is default or not
    [SerializeField] public bool isDefaultAbility = false;
   
    //Type of Ability
    [SerializeField] public abilityType type;
    
    // Discription
    [TextArea(15, 20)]
    [SerializeField] private string discription;

    [SerializeField] private string prefabName;
    [SerializeField] private float coolDown;

    //These 
    

    //These are getters for the Class
    public string Name => name;
    public Sprite Icon => icon;
   
    public string Discription => discription;
    public string PrefabName => prefabName;
    public float CoolDown => coolDown;
    
    





}