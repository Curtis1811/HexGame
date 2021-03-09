using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Status")]
public class StatusEffects : SpellEffects
{
    SpellEffectType type = SpellEffectType.Status;
    // Start is called before the first frame update
    public override void ExceuteEffect(Fireabilities ability, GameObject go, GameObject playerPrefab)
    {
        Instantiate<GameObject>(go,playerPrefab.transform.position,Quaternion.identity);
                
        throw new System.NotImplementedException();
    }

}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Wall")]
public class WallEffects  : SpellEffects
{
    SpellEffectType type = SpellEffectType.Wall;
    public override void ExceuteEffect(Fireabilities ability, GameObject go, GameObject playerPrefab)
    {


        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/Projectile")]
public class ProjectileEffects : SpellEffects
{
    SpellEffectType type = SpellEffectType.Projectile;
    public override void ExceuteEffect(Fireabilities ability, GameObject go, GameObject playerPrefab)
    {


        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/AOE")]
public class AOE : SpellEffects {
    SpellEffectType type = SpellEffectType.AOE;
    public override void ExceuteEffect(Fireabilities ability, GameObject go, GameObject PlayerPrefab)
    {
        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Effects", menuName = "EffectSystem/Effects/DOT")]
public class DOTEffects : SpellEffects {
    SpellEffectType type = SpellEffectType.DOT;
    public override void ExceuteEffect(Fireabilities ability, GameObject go, GameObject PlayerPrefab)
    {
        throw new System.NotImplementedException();
    }
}


/*
    Status,
    Wall,
    Projectile,
    AOE,
    DOT*/
