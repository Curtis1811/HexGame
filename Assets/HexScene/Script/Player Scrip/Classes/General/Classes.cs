using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public abstract class Classes : NetworkBehaviour
{

    #region Delegates
    public delegate void keyUp();
    public keyUp onKeyUp;

    #endregion

    public string[] abilityData = new string[4]; // this hold the data from the Netowork manager
    public List<Abilities> ChosenAbilityList = new List<Abilities>();
    
    [TextArea(10, 30)]
    public string discription = "This is a Basic Class!";
    public string nameOfClass = "Basic Class";
    //Here we are defining all the game objects that the Hydromancer class will use
    //These will need to be loaded from the resource folder
    [Header("GameObjects")]
    [SerializeField] public GameObject AbilityOnePrefab;
    [SerializeField] public GameObject AbilityTwoPrefab;
    [SerializeField] public GameObject AbilityThreePrefab;
    [SerializeField] public GameObject AbilityFourPrefab;
    [SerializeField] public GameObject CastPoint;
    [SerializeField] CooldownSystem CD_system; //The cooldown Systen is here so we can use it on the different abilities
    //here we may add the Adrenaline Prefab. Maybe Not
    //[SerializeField] GameObject fireWavePrefab;

    [Header("Class Variables")]
    public List<string> AbilityNames;
    NetworkConnection connection;
    public PlayerMovement playermove;
    float cooldownreduction;
    float FireRate;

    float currentChargeTime;
    
    //We will change input into an event system,
    [Client]
    public virtual void keyChecker(bool CanShoot){
        if(CanShoot){ //If the player can shoot.
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {   
                //GameObjectSpawnPointSetUp(ChosenAbilityList[0]);
            }
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                GameObjectSpawnPointSetUp(ChosenAbilityList[1]);
                checkSpellType(ChosenAbilityList[1]);
            }   
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObjectSpawnPointSetUp(ChosenAbilityList[2]);
                checkSpellType(ChosenAbilityList[2]);
            }   
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameObjectSpawnPointSetUp(ChosenAbilityList[3]);
                checkSpellType(ChosenAbilityList[3]);
            }       
        }
    }

    public void keyUpEvent(){
        //When the key has been released we are going to fire the event.
        

    }

    //This system needs to change as what happens if we have a selfcast 
    void ChargeSpell(Abilities ability){// here we may want to add a key code variable
    
        //Since we are checking if the key is down in the keychecker 
        //We are going to check when the button is up.
        //Here we are going to define the charging perameters
        
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("Charging, Character cannot perform any other move");
            return;
        }else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Charged and Fire");  
        }

        /*
        if(CD_system.isOnCooldown(ability.SpellId)){
            return;
        }
               
        CmdAbilityOnePrefab(ability.spawnPoint);
        Debug.Log("This is a charge spell: Showed while charging" + ability.name);
        CD_system.PutOnCooldown(ability);*/
    }

    void NormalAbility(Abilities ability)
    {
        if(CD_system.isOnCooldown(ability.SpellId)){
            return;
        }

        CmdAbilityOnePrefab(ability.spawnPoint);
        Debug.Log("Normal Spells: slowed briefly while casting" + ability.name);
        CD_system.PutOnCooldown(ability);
    }
 
    public void checkSpellType(Abilities ability){
        switch (ability.isCharge)
        {
            case true:
                ChargeSpell(ability);
                break;

            case false:
                NormalAbility(ability);
                break;
                    
            }
    }

    //These are fine since we awant to handel the WAY we spawn in the normal ability and Charge Ability Function
    [Command]
    void CmdAbilityOnePrefab(Vector3 Position){
        GameObject ObjectToSpawn = Instantiate(AbilityOnePrefab, Position, this.transform.rotation);
        //rocksmash.transform.position += Vector3.forward;
        ObjectToSpawn.GetComponent<SpellBehavior>().timer = NetworkTime.time;
        ObjectToSpawn.GetComponent<SpellBehavior>().playerWhoSpawned = this.gameObject;
        ObjectToSpawn.GetComponent<SpellBehavior>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(ObjectToSpawn,this.gameObject);
        ClientScene.RegisterPrefab(ObjectToSpawn);
    }



    void CmdAbilityTwoPrefab(Vector3 Position, Quaternion Rotation){
        GameObject ObjectToSpawn = Instantiate(AbilityTwoPrefab, CastPoint.transform.position, playermove.transform.rotation);
        //rocksmash.transform.position += Vector3.forward;
        ObjectToSpawn.GetComponent<SpellBehavior>().timer = NetworkTime.time;
        ObjectToSpawn.GetComponent<SpellBehavior>().playerWhoSpawned = this.gameObject;
        ObjectToSpawn.GetComponent<SpellBehavior>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(ObjectToSpawn,this.gameObject);
        ClientScene.RegisterPrefab(ObjectToSpawn);
    }
    void CmdAbilityThreePrefab(Vector3 Position, Quaternion Rotation){
        GameObject ObjectToSpawn = Instantiate(AbilityThreePrefab, CastPoint.transform.position, playermove.transform.rotation);
        //rocksmash.transform.position += Vector3.forward;
        ObjectToSpawn.GetComponent<SpellBehavior>().timer = NetworkTime.time;
        ObjectToSpawn.GetComponent<SpellBehavior>().playerWhoSpawned = this.gameObject;
        ObjectToSpawn.GetComponent<SpellBehavior>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(ObjectToSpawn,this.gameObject);
        ClientScene.RegisterPrefab(ObjectToSpawn);
    }
    void CmdAbilityFourPrefab(Vector3 Position, Quaternion Rotation){
        GameObject ObjectToSpawn = Instantiate(AbilityFourPrefab, Position, playermove.transform.rotation);
        //rocksmash.transform.position += Vector3.forward;
        ObjectToSpawn.GetComponent<SpellBehavior>().timer = NetworkTime.time;
        ObjectToSpawn.GetComponent<SpellBehavior>().playerWhoSpawned = this.gameObject;
        ObjectToSpawn.GetComponent<SpellBehavior>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(ObjectToSpawn,this.gameObject);
        ClientScene.RegisterPrefab(ObjectToSpawn);
    }

    public void GameObjectSpawnPointSetUp(Abilities abilities)
    {
        //This is to set up the spawn points of the different abilties
        switch (abilities.SpawnType)
        {
            case SpawnType.SpawnOnSelf:
                abilities.spawnPoint = this.transform.position;
                break;
            case SpawnType.SpawnOnCastPoint:
                abilities.spawnPoint = CastPoint.transform.position;
                break;
            case SpawnType.SpawnOnTargetPoint:
                abilities.spawnPoint = playermove.targetPoint; 
                break;

            default:
                Debug.LogError("There is no spawn Posiiton for this");
                abilities.spawnPoint = new Vector3(0,0,0);
                break;
        }
        
    }
}
