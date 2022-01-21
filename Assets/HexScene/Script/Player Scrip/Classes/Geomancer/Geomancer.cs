using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Geomancer : Classes
{
    // Start is called before the first frame update
    //[TextArea(10, 30)]   
    [Header("GameObjects")]
    
    GeomancerHandler gh;
 

    private void Awake()
    {   
        gh = GetComponent<GeomancerHandler>();
        playermove = this.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        ChosenAbilityList = gh.EarthChosenList;
        
        gh.assignGameObjects();
        //here we assing the  prefabs
        //AbilityOnePrefab = gh.GeomancerGameObjectPrefab.Find(x => x.name == "Rocksmash");
        //AbilityTwoPrefab = gh.GeomancerGameObjectPrefab.Find(x => x.name == "MudWall");
        //AbilityThreePrefab = gh.GeomancerGameObjectPrefab.Find(x => x.name == "HexCraft");
        //AbilityFourPrefab = gh.GeomancerGameObjectPrefab.Find(x => x.name == "EarthClap");
        //----- pog
        ChosenAbilityList.Add(gh.ChosenListOfAbilities.Find(x => x.name == "Rocksmash"));
        ChosenAbilityList.Add(gh.ChosenListOfAbilities.Find(x => x.name == "MudWall"));
        ChosenAbilityList.Add(gh.ChosenListOfAbilities.Find(x => x.name == "EarthClap"));
        ChosenAbilityList.Add(gh.ChosenListOfAbilities.Find(x => x.name == "HexCraft"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
            keyChecker(playermove.CanShoot);
    }
    /*
    [Client]
    public void keyChecker(bool CanShoot)
    {
        if (CanShoot == true) {
            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if(CD_system.isOnCooldown(RocksmashPrefab.GetComponent<RockSmash>().earthAbilities.id)){
                       return;
                    }
                    AbilityCharging(1f);
                    Debug.Log("Changing Speed of Player to base");
                    //Here we want to add some kind of charge mechanic, We are going 
                }
                 
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if(CD_system.isOnCooldown(RocksmashPrefab.GetComponent<RockSmash>().earthAbilities.id)){
                       return;
                    }
                    AbilityCharging(2f);
                    CmdRockSmash(this.transform.position);
                    CD_system.PutOnCooldown(RocksmashPrefab.GetComponent<RockSmash>().earthAbilities);
                
                }
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(MudWallPrefab.GetComponent<MudWall>().earthAbilities.id);
                    
                    if (CD_system.isOnCooldown(MudWallPrefab.GetComponent<MudWall>().earthAbilities.id))
                    {
                        Debug.Log(CD_system.getRemaningDuration(MudWallPrefab.GetComponent<MudWall>().earthAbilities.id));
                        return;
                    }

                    CmdMudWall(playermove.targetPoint);
                    CD_system.PutOnCooldown(MudWallPrefab.GetComponent<MudWall>().earthAbilities);
                    //Here is where we put the ability on cooldown
                }

                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Debug.Log(MudWallPrefab.GetComponent<MudWall>().earthAbilities.id);
                    if (CD_system.isOnCooldown(HexCraftPrefab.GetComponent<Hexcraft>().earthAbilities.id))//here is whgere we check to see iof the ability is on cooldown)
                    {
                        Debug.Log(CD_system.getRemaningDuration(HexCraftPrefab.GetComponent<Hexcraft>().earthAbilities.id));
                        return;
                    }

                    CmdHexCraft(playermove.targetPoint);
                    CD_system.PutOnCooldown(HexCraftPrefab.GetComponent<Hexcraft>().earthAbilities);
                    //Here is where we put the ability on cooldown
                }
                
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    if (CD_system.isOnCooldown(EarthClapPrefab.GetComponent<EarthClap>().earthAbilities.id))//here is whgere we check to see iof the ability is on cooldown)
                    {
                        Debug.Log(CD_system.getRemaningDuration(EarthClapPrefab.GetComponent<EarthClap>().earthAbilities.id));
                        return;
                    }

                    CmdEarthClap(playermove.targetPoint);               
                    CD_system.PutOnCooldown(EarthClapPrefab.GetComponent<EarthClap>().earthAbilities);
                }
            }
        }
        
    }*/

    #region Client
        
    [Command]
    void CmdRockSmash(Vector3 MousePosition){
        GameObject rocksmash = NetworkAnimator.Instantiate(AbilityOnePrefab, CastPoint.transform.position, playermove.transform.rotation);
        //rocksmash.transform.position += Vector3.forward;
        rocksmash.GetComponent<SpellBehavior>().timer = NetworkTime.time;
        rocksmash.GetComponent<SpellBehavior>().playerWhoSpawned = this.gameObject;
        rocksmash.GetComponent<SpellBehavior>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(rocksmash,this.gameObject);
        ClientScene.RegisterPrefab(rocksmash);
    }

    [Command]
    void CmdMudWall(Vector3 MousePosition){
        GameObject mudwall = NetworkAnimator.Instantiate(AbilityTwoPrefab, playermove.targetPoint, playermove.transform.rotation);
        mudwall.GetComponent<MudWall>().playerWhoSpawned = this.gameObject;
        mudwall.GetComponent<MudWall>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(mudwall,this.gameObject);
        ClientScene.RegisterPrefab(mudwall);

    }

    [Command]
    void CmdHexCraft(Vector3 MousePosition){
        GameObject HexCraft = NetworkAnimator.Instantiate(AbilityThreePrefab,new Vector3(playermove.targetPoint.x,0, playermove.targetPoint.z), Quaternion.identity);
        HexCraft.GetComponent<Hexcraft>().playerWhoSpawned = this.gameObject;
        HexCraft.GetComponent<Hexcraft>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(HexCraft,this.gameObject);
        ClientScene.RegisterPrefab(HexCraft);
    }

    [Command]
    void CmdEarthClap(Vector3 MousePosition){
        GameObject earthclap =  NetworkAnimator.Instantiate(AbilityFourPrefab, playermove.targetPoint, playermove.transform.rotation);
        earthclap.GetComponent<EarthClap>().playerWhoSpawned = this.gameObject;
        earthclap.GetComponent<EarthClap>().SpawnedNetId  = this.netId;
        NetworkServer.Spawn(earthclap,this.gameObject);
        ClientScene.RegisterPrefab(earthclap);
    }
    #endregion

    void AbilityCharging(float x){
        playermove.CmdChangeSpeed(x);
    }


    void SpellActivationBehavior(){

    }


}
