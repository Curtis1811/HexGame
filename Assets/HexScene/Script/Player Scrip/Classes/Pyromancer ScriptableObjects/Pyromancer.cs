using Microsoft.Win32;
//using Mono.CecilX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//here we will have function delaget to take in the right Abilites to use.
public class Pyromancer : NetworkBehaviour //MonoBehaviour//PyromancerHandler
{
    string[] abil = new string[4];
    [TextArea(10, 30)]
    public string discription = "Fire is good and strong yes!";
    //Here we are defining all the game objects that the pyromancer class will use

    //These will need to be loaded from the resource folder
    [Header("GameObjects")]
    [SerializeField] public GameObject fireballPrefab;
    [SerializeField] public GameObject MeteorPrefab;
    [SerializeField] public GameObject LargeFireBallPrefab;
    [SerializeField] public GameObject AdrnalinePrefab;
    CooldownSystem cooldown;

    //here we may add the Adrenaline Prefab. Maybe Not
    //[SerializeField] GameObject fireWavePrefab;

    [Header("Pyromancer Variables")]
    public List<string> AbilityNames;
    PyromancerHandler ph;
    NetworkConnection connection;
    float cooldownreduction; // this will Dictate the speed at whch an ability can be used
    float FireRate; // This will be Global Cooldown
    PlayerMovement playermove;
    // This will be assigned based on what Abilites are 
    //Change this to have its own Class that other classes can use as a tempalte.
    //This will need to gain the .txt data on load and recieve the data to use.

    void Awake()
    {
        cooldown = this.GetComponent<CooldownSystem>();
        ph = GetComponent<PyromancerHandler>();
        playermove = this.GetComponent<PlayerMovement>();
        //We will need to loop though some prefabs and assign them to the correct variables
    }
    void Start()
    {
        fireballPrefab = ph.PyromancerGameObjectPrefabs.Find(x => x.name == "FireBall");
        MeteorPrefab = ph.PyromancerGameObjectPrefabs.Find(x => x.name == "Meteor");
        LargeFireBallPrefab = ph.PyromancerGameObjectPrefabs.Find(x => x.name == "LargeFireBall");
        AdrnalinePrefab = ph.PyromancerGameObjectPrefabs.Find(x => x.name == "AdrenalinePrefab");
        ClientScene.RegisterPrefab(fireballPrefab);
        ClientScene.RegisterPrefab(AdrnalinePrefab);
        ClientScene.RegisterPrefab(MeteorPrefab);
        ClientScene.RegisterPrefab(LargeFireBallPrefab);
        //KeyAssinging(PyromancerChosenList);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
            keyChecker(playermove.CanShoot);
    }


    [Client]
    public void keyChecker(bool CanShoot)
    {
        if (isLocalPlayer && CanShoot) { 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (cooldown.isOnCooldown(fireballPrefab.GetComponent<FireBall>().fireabilities.id))
                {
                    return;
                }
                CmdFireball(ClientScene.localPlayer.GetComponent<PlayerMovement>().targetPoint, netId);
                cooldown.PutOnCooldown(fireballPrefab.GetComponent<FireBall>().fireabilities);
                //Debug.Log(ph.PyromancerChosenList[0]);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (cooldown.isOnCooldown(LargeFireBallPrefab.GetComponent<LargeFireBall>().fireabilities.id))
                {
                    return;
                }
                CmdLargeFireBall(playermove.targetPoint); //(this.GetComponent<PlayerMovement>().targetPoint);
                cooldown.PutOnCooldown(LargeFireBallPrefab.GetComponent<LargeFireBall>().fireabilities);
                //Debug.Log(ph.PyromancerChosenList[1]);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cooldown.isOnCooldown(AdrnalinePrefab.GetComponent<Adrenaline>().abilities.id))
                {
                    return;
                }
                CmdAdrnaline(this.netId);
                cooldown.PutOnCooldown(AdrnalinePrefab.GetComponent<Adrenaline>().fireabilities);
                //Debug.Log(ph.PyromancerChosenList[2].name);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (cooldown.isOnCooldown(MeteorPrefab.GetComponent<Meteor>().fireabilities.id))
                {
                    return;
                }
                CmdMeteor(ClientScene.localPlayer.GetComponent<PlayerMovement>().targetPoint);
                cooldown.PutOnCooldown(MeteorPrefab.GetComponent<Meteor>().fireabilities);
                //Debug.Log(ph.PyromancerChosenList[3].name);    
            }
        }
    }
    
    public void KeyAssinging(List<Fireabilities> ChosenList)
    {
            
    }

    //THis stuff is placeHolder To make sure the ability spawn and work. Now they do we are doing to change the code so we are spawning and accessing
    //The abilities in a smarter and cleaner way
    //These may need to be put into a different script. THIS IS FOR TESTING
    [Command]
    public void CmdFireball(Vector3 mousePosition, uint id)
    {
        GameObject fireball = Instantiate(fireballPrefab, this.transform.position, this.transform.rotation);
        fireball.GetComponent<FireBall>().x = mousePosition.x;
        fireball.GetComponent<FireBall>().y = mousePosition.y;
        fireball.GetComponent<FireBall>().z = mousePosition.z;
        fireball.GetComponent<FireBall>().SpawnedNetId = id;
        fireball.GetComponent<FireBall>().timer = NetworkTime.time;
        NetworkServer.Spawn(fireball, this.gameObject);
        //ClientScene.RegisterPrefab(fireball);     
    }

    [Command]
    public void CmdMeteor(Vector3 mousePosition)
    {
        //Vector3 SpawnPosition
        GameObject Meteor = Instantiate(MeteorPrefab,new Vector3(this.transform.position.x,35, this.transform.position.z), Quaternion.identity);
        Meteor.GetComponent<Meteor>().x = mousePosition.x;
        Meteor.GetComponent<Meteor>().y = mousePosition.y;
        Meteor.GetComponent<Meteor>().z = mousePosition.z;
        Meteor.GetComponent<Meteor>().SpawnedNetId = this.netId;
        Meteor.GetComponent<Meteor>().timer = NetworkTime.time;
        //These are to register the Meteor GameObject to Client and Server
        NetworkServer.Spawn(Meteor, this.gameObject);
        
    }

    [Command]
    public void CmdAdrnaline(uint player)
    {
        //System.Guid creatureAssetId = System.Guid.NewGuid();
        GameObject adrenaline = Instantiate(AdrnalinePrefab, this.transform.position, Quaternion.identity);
        //adrenaline.transform.parent = this.transform;
        adrenaline.GetComponent<Adrenaline>().SpawnedNetId = player;
        adrenaline.GetComponent<Adrenaline>().timer = NetworkTime.time;
        NetworkServer.Spawn(adrenaline, this.gameObject);

    }

    [Command]
    public void CmdLargeFireBall(Vector3 MousePosition)
    {
        Debug.Log(MousePosition  + " MOUSE POSITION");

        //Vector3 SpawnPosition
        GameObject LargeFireBall = Instantiate(LargeFireBallPrefab, this.transform.position, this.transform.rotation);
        LargeFireBall.GetComponent<LargeFireBall>().x = MousePosition.x;
        LargeFireBall.GetComponent<LargeFireBall>().y = MousePosition.y;
        LargeFireBall.GetComponent<LargeFireBall>().z = MousePosition.z;
        LargeFireBall.GetComponent<LargeFireBall>().SpawnedNetId = this.netId;
        LargeFireBall.GetComponent<LargeFireBall>().timer = NetworkTime.time;
       
        //These are to register the Meteor GameObject to Client and Server
        NetworkServer.Spawn(LargeFireBall, this.gameObject);
        
    }

}