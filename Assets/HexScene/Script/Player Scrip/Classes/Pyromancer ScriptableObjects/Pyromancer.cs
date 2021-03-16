using Microsoft.Win32;
//using Mono.CecilX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//here we will have function delaget to take in the right Abilites to use.
public class Pyromancer : NetworkBehaviour//MonoBehaviour//PyromancerHandler
{
    string[] abil = new string[4];

    [TextArea(10, 30)]  
    public string discription = "Fire is good and strong yes!";
    public new string name = "Pyromancer";
    //Here we are defining all the game objects that the pyromancer class will use

    //These will need to be loaded from the resource folder
    [Header("GameObjects")]
    [SerializeField] public GameObject fireballPrefab;
    [SerializeField] GameObject fireWavePrefab;
    [SerializeField] public GameObject MeteorPrefab;

    [Header("Pyromancer Variables")]
    public List<string> AbilityNames;
    PyromancerHandler ph;
    NetworkConnection connection;
    float cooldownreduction;
    // This will be assigned based on what Abilites are Selected

    //Change this to have its own Class that other classes can use as a tempalte.

    //This will need to gain the .txt data on load and recieve the data to use.

    private void Awake()
    {
        ph = GetComponent<PyromancerHandler>();
        
        //We will need to loop though some prefabs and assign them to the correct variables
    }
    void Start()
    {
        fireballPrefab = ph.PyromancerGameObjectPrefabs.Find(x => x.name == "FireBall");
        //KeyAssinging(PyromancerChosenList);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
            keyChecker();
    }

    [Client]
    public void keyChecker()
    {
        if (isLocalPlayer) { 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CmdFireball(this.gameObject.GetComponent<PlayerMovement>().targetPoint);                   
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CmdMeteor(this.gameObject.GetComponent<PlayerMovement>().targetPoint);
                Debug.Log("Firing");
                //Debug.Log(ph.PyromancerChosenList[1]);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(ph.PyromancerChosenList[2].name);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log(ph.PyromancerChosenList[3].name);
            }
        }
    }

    public void KeyAssinging(List<Fireabilities> ChosenList)
    {
            
    }

    [ClientRpc]
    public void RpcFireball(GameObject fireball) 
    {
        
    }

    //These may need to be put into a different script. THIS IS FOR TESTING
    [Command]
    public void CmdFireball(Vector3 MousePosition)
    {
        Vector3 SpawnPosition = GetComponent<PlayerMovement>().gameObject.transform.position;
        GameObject fireball = Instantiate(fireballPrefab, SpawnPosition, Quaternion.identity);
        fireball.GetComponent<FireBall>().ProjectileDirection = MousePosition;
        //This seems to work and spawns on the Client
        NetworkServer.Spawn(fireball,this.gameObject);
        //fireball.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToServer);
        ClientScene.RegisterPrefab(fireball);        
    }

    [Command]
    public void CmdMeteor(Vector3 MousePosition)
    {
        //Vector3 SpawnPosition
        GameObject Meteor = Instantiate(MeteorPrefab,new Vector3(this.transform.position.x,35, this.transform.position.z), Quaternion.identity);
        Meteor.GetComponent<Meteor>().MovementDirection = MousePosition;
        //These are to register the Meteor GameObject to Client and Server
        NetworkServer.Spawn(Meteor, this.gameObject);
        ClientScene.RegisterPrefab(Meteor);
    }

    public void cmdFireWave()
    {
        Debug.Log("FireWave");
        //Here wea re going to spawn the FireWave
    }

    public void cmdAdrnaline()
    {//This ability should reduce the cooldowns of all abilites
        //Here we need a refereance to the playerMovementScript
        //StartCoroutine
        //this.GetComponent<PlayerMovement>().
    }

}