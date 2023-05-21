using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hydromancer : NetworkBehaviour 
{
    string[] abil = new string[4];
    [TextArea(10, 30)]
    public string discription = "Water!";
    public new string name = "Hydromancer";
    //Here we are defining all the game objects that the Hydromancer class will use

    //These will need to be loaded from the resource folder
    [Header("GameObjects")]
    [SerializeField] public GameObject ConePrefab;
    [SerializeField] public GameObject WavePrefab;
    [SerializeField] public GameObject WhirlPoolPrefab;
    [SerializeField] public GameObject BubbleShieldPrefab;
    [SerializeField] CooldownSystem CD_system;
    //here we may add the Adrenaline Prefab. Maybe Not
    //[SerializeField] GameObject fireWavePrefab;

    [Header("Hydromancer Variables")]
    public List<string> AbilityNames;
    HydromancerHandler hh;
    NetworkConnection connection;
    float cooldownreduction;
    float FireRate;

    PlayerMovement playermove;

    // Start is called before the first frame update
    private void Awake()
    {
        hh = GetComponent<HydromancerHandler>();
        playermove = this.GetComponent<PlayerMovement>();
        //We will need to loop though some prefabs and assign them to the correct variables
    }
    void Start()
    {
        ConePrefab = hh.HydromancerGameObjectPrefabs.Find(x => x.name == "WaterCone");
        WavePrefab = hh.HydromancerGameObjectPrefabs.Find(x => x.name == "Wave");
        WhirlPoolPrefab = hh.HydromancerGameObjectPrefabs.Find(x => x.name == "WhirlPool");
        BubbleShieldPrefab = hh.HydromancerGameObjectPrefabs.Find(x => x.name == "BubbleShield");

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
            keyChecker(playermove.CanShoot);
    }

    [Client]
    public void keyChecker(bool CanShoot)
    {
        if (CanShoot == true) {
            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Debug.Log(ph.PyromancerChosenList[0]);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (CD_system.isOnCooldown(WavePrefab.GetComponent<WaterWave>().WaterAbilities.id))
                    {

                        Debug.Log("OnCooldown");
                        return;
                    }
                    CmdWave(playermove.targetPoint);
                    CD_system.PutOnCooldown(WavePrefab.GetComponent<WaterWave>().WaterAbilities);
                    //Debug.Log(ph.PyromancerChosenList[1]);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(WhirlPoolPrefab.GetComponent<Whirlpool>().WaterAbilities.id);
                    
                    if (CD_system.isOnCooldown(WhirlPoolPrefab.GetComponent<Whirlpool>().WaterAbilities.id))
                    {

                        Debug.Log("OnCooldown");
                        return;
                    }
                    CmdWhirlPool(playermove.targetPoint);
                    CD_system.PutOnCooldown(WhirlPoolPrefab.GetComponent<Whirlpool>().WaterAbilities);
                    //Debug.Log(ph.PyromancerChosenList[2].name);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (CD_system.isOnCooldown(BubbleShieldPrefab.GetComponent<BubbleShield>().WaterAbilities.id))
                    {
                        Debug.Log("OnCooldown");
                        return;
                    }
                    // This will generally Be your Defensive Manuver
                    CmdBubbleShield();
                    CD_system.PutOnCooldown(BubbleShieldPrefab.GetComponent<BubbleShield>().WaterAbilities);
                    //Debug.Log(ph.PyromancerChosenList[3].name);    
                }
               
            }
        }
        
    }

    #region Client  
    [Command]
    void CmdWave(Vector3 MousePosition)
    {
        GameObject Wave = Instantiate(WavePrefab, this.transform.position, this.transform.rotation);

        Wave.GetComponent<WaterWave>().x = MousePosition.x;
        Wave.GetComponent<WaterWave>().y = MousePosition.y;
        Wave.GetComponent<WaterWave>().z = MousePosition.z;
        Wave.GetComponent<WaterWave>().timer = NetworkTime.time;
        Wave.GetComponent<WaterWave>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(Wave, this.gameObject);
        NetworkClient.RegisterPrefab(Wave);
    }

    [Command]
    void CmdWhirlPool(Vector3 MousePosition)
    {
        GameObject WhirlPool = Instantiate(WhirlPoolPrefab, playermove.targetPoint, Quaternion.identity);
        WhirlPool.GetComponent<Whirlpool>().x = MousePosition.x;
        WhirlPool.GetComponent<Whirlpool>().y = MousePosition.y;
        WhirlPool.GetComponent<Whirlpool>().z = MousePosition.z;
        WhirlPool.GetComponent<Whirlpool>().timer = NetworkTime.time;
        WhirlPool.GetComponent<Whirlpool>().SpawnedNetId = this.netId;
        NetworkServer.Spawn(WhirlPool, this.gameObject);
        NetworkClient.RegisterPrefab(WhirlPool);

    }

    [Command]
    void CmdBubbleShield()
    {
        GameObject bubbleShield = Instantiate(BubbleShieldPrefab, this.transform.position, Quaternion.identity);
        bubbleShield.GetComponent<BubbleShield>().PlayerWhoSpawned = this.gameObject;
        bubbleShield.GetComponent<BubbleShield>().SpawnedNID = this.netId;
        NetworkServer.Spawn(bubbleShield, this.gameObject);
        NetworkClient.RegisterPrefab(bubbleShield);
    }

    #endregion


    #region Server


    #endregion


    #region Misc


    #endregion

}
