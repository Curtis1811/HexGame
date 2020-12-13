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
    [SerializeField] public GameObject fireballPrefab;
    [SerializeField] GameObject fireWavePrefab;

    public List<string> AbilityNames;
    PyromancerHandler ph;
    // Start is called before the first frame update

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
                CmdFireball();                   
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Firing");
                Debug.Log(ph.PyromancerChosenList[1]);
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
    public void RpcFireball(GameObject fireball) {
       
        
    }

    [Command]
    public void CmdFireball()
    {
        Vector3 SpawnPosition = GetComponent<PlayerMovement>().gameObject.transform.position;
        GameObject fireball = Instantiate(fireballPrefab, SpawnPosition, Quaternion.identity);
        RpcFireball(fireball);
        NetworkServer.Spawn(fireball);
        ClientScene.RegisterPrefab(fireball);
        Debug.Log("CMD");
       
    }

}


