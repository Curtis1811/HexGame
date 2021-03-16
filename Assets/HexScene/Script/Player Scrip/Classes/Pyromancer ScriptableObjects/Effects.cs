using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Status")]
public class StatusEffects : SpellEffects
{
    SpellEffectType type = SpellEffectType.Status;
    // Start is called before the first frame update
    public override void ExceuteEffect(Abilities ability, GameObject go, GameObject playerPrefab)
    {
        Debug.Log("Ths is a Status SpellEffect");
                
       
    }

}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Wall")]
public class WallEffects  : SpellEffects
{
    SpellEffectType type = SpellEffectType.Wall;
    public override void ExceuteEffect(Abilities ability, GameObject go, GameObject playerPrefab)
    {
        Debug.Log("This is a wall effect");

    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Projectile")]
public class ProjectileEffects : SpellEffects
{
    SpellEffectType type = SpellEffectType.Projectile;
    public override void ExceuteEffect(Abilities ability, GameObject go, GameObject playerPrefab)
    {
        //Instantiate<GameObject>(go,playerPrefab.transform.position,Quaternion.identity);
        Debug.Log("This is a projectile Effect");
       //throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/AOE")]
public class AOE : SpellEffects {
    SpellEffectType type = SpellEffectType.AOE;
    public override void ExceuteEffect(Abilities ability, GameObject go, GameObject PlayerPrefab)
    {
        Debug.Log("This is an AOE effect");
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/DOT")]
public class DOTEffects : SpellEffects {
    SpellEffectType type = SpellEffectType.DOT;
    public override void ExceuteEffect(Abilities ability, GameObject go, GameObject PlayerPrefab)
    {

        Debug.Log("THis is a DOT effect");
    }
}


/*
    Status,
    Wall,
    Projectile,
    AOE, 
    DOT*/
