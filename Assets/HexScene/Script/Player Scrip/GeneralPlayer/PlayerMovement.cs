using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using Mirror;
using System;

public class PlayerMovement : NetworkBehaviour
{
    public UnityEngine.Camera playerCamera;
    public GameObject playerCameraGameObject;
    public GameObject playerCameraGameObjectReferance;
    public float playerYSpeed;
    [Header("NetworkVariables")]
    //This Health is for Testing; CHANGE THESE TO PROTECTED AND USD FUNCTIONS TO CHANGE THEM
    [SyncVar]
    public float health; // players health
    [SyncVar]
    public int stamina; // this is for player dodge
    [SyncVar]
    public float scaler; // this is speed
    public float OriginalScaler; // this is the players Original Speed
    [SyncVar]
    public float knockback;
    bool isAlive;
    bool isReady;
    [SyncVar]
    public bool CanShoot;
    public float knockbackTimer;

    [Header("Network Stuff")]    //We need to add a stun effect
    public Vector3 targetPoint;
    int NetworkId;


    //Here we are setting up delagets TESTING
    public delegate void SpeedChange(float SpeedAmount);
    public delegate void Stunned(bool stunned);
    public delegate void DamageRecieved(float Damage);


    // These used to be Sync events. Documentation says to use Cmds instead https://github.com/MirrorNetworking/Mirror/pull/2178
    //Todo: Change these to CLientRPC and SercerRPC respectivly
    public event SpeedChange EventOnSpeedChange;

    public event Stunned EventOnStunned;

    public event DamageRecieved EventOnDamageRecieved;
   
   
    [SyncVar]
    public Vector3 testVector;

    private void Awake()
    {
        health = 0;
        stamina = 100;
        knockback = 0;
        scaler = 2f;
        OriginalScaler = 2;
        CanShoot = true;
    }

    void Start() {
        playerYSpeed = 0;
        //playerCameraGameObjectReferance = Instantiate<GameObject>(playerCameraGameObject, this.transform.position, Quaternion.identity) as GameObject;
        knockbackTimer = Time.time;
        //NetworkServer.Spawn(playerCameraGameObjectReferance, this.gameObject);

        if (isLocalPlayer) {
            playerCameraGameObjectReferance = Instantiate<GameObject>(playerCameraGameObject, this.transform.position, Quaternion.identity) as GameObject;
            //NetworkServer.Spawn(playerCameraGameObjectReferance, this.gameObject);
            playerCameraGameObjectReferance.SetActive(true);
            playerCameraGameObjectReferance.GetComponent<CameraScript>().player = this.gameObject.transform;
            playerCamera = playerCameraGameObjectReferance.GetComponent<Camera>();
            //playerCamera.transform.rotation = new Vector3(90, 90, 0);
        }
        else
        {
            //playerCameraGameObjectReferance.SetActive(false);
        }
    }

    //Update is called once per frame
    [Client]
    void Update() {

        if (!hasAuthority)
            return;

        if (isLocalPlayer) {
            if (CanShoot == true){
                movement();
                faceMouse();
            }
            
            CmdUpdatePlayerPosition(this.transform.position, this.transform.rotation);
            isBeingKnockedBack();
            checkPlayerHeight();
        }
    }

    [Client]
    private void checkPlayerHeight()
    {
        if (transform.position.y < 1.7f) {
            Debug.Log(transform.position.y);
            playerYSpeed = -10;
        }
        else
        {
            playerYSpeed = 0;
        }
    }

    [Client]
    void movement()
    {
        //sprint();
        //Here we have tom check and see if we need to apply KockBack; If this is the case we need to stop the player from moving while they are being knocked back.

        //this.transform.position += testVector* Time.deltaTime * knockback; //Need way more refining.
        //Here what wsprinte want to do is reduce the knockback until 0

        if (Input.GetKey("w"))
        {
            this.transform.position += new Vector3(5, playerYSpeed, 0) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("s"))
        {
            this.transform.position += new Vector3(-5, playerYSpeed, 0) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("d"))
        {
            this.transform.position += new Vector3(0, playerYSpeed, -5) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("a"))
        {
            this.transform.position += new Vector3(0, playerYSpeed, 5) * Time.deltaTime * scaler;
        }

    }
    public void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            scaler = OriginalScaler + 3;
        }
        else
        {
            scaler = OriginalScaler;
        }
    }

    [Client]
    void faceMouse()
    {
        Plane PlayerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        //When Using Camera If you have a camera scrip you Must use unity engine infront of it
        //Debug.Log(ray.direction);
        float hitDistance = Mathf.Infinity;
        if (PlayerPlane.Raycast(ray, out hitDistance))
        {
            targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);

            Debug.DrawLine(ray.origin, targetPoint, Color.red);
            //This does work But we need to give the player a new camera
        }
    }

    [Client]
    void isBeingKnockedBack()
    {

        this.transform.position += testVector * Time.deltaTime * knockback;
        if (knockback == 0)
            testVector = new Vector3(0, 0, 0);
        CmdKnockBack(knockback);

        if (Time.time >= knockbackTimer + 3f)
            if (knockback > 0)
                knockbackTimer = Time.time;

        knockback = Mathf.Max(knockback - 0.3f, 0f);

        //We want to do the calculation to reduce the kickback every 0.3 seconds by 1
        // once it reaches zero we set being kocked back to false and we set the knockback direction to 0,0,0;
    }

    //Here we are doing to create some functions to get the mouse world Position Since all player objects will ahve a playmovements we can pass this to any class The player has
    //May have to move this to Different classes. 
    //WE are creating this here becasye we ahve a Face mouse Function that has data. 
    [Client]
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 TempVec = GetMouseWorldPositionWithZ(Input.mousePosition, UnityEngine.Camera.main);
        TempVec.y = 0;
        return TempVec;
    }

    [Client]
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, UnityEngine.Camera mainCamera)
    {
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(screenPosition);
        return tempVec;
    }


    #region Synced Events
    public void setSpeed(float SpeedAmount)
    {
        scaler = SpeedAmount;
        EventOnSpeedChange?.Invoke(SpeedAmount);
    }

    //public void CmdSetSpeed() => setSpeed(3);

   
    public void setStun(bool Stunned, float time)
    {
        CanShoot = Stunned;
        EventOnStunned?.Invoke(CanShoot);
        CmdEffectTiming(time);

    }
  
    public void ApplyDamage(float Damage)
    {
        Debug.Log("Apply Damage: " + Damage);
        health += Damage;
        EventOnDamageRecieved?.Invoke(Damage);
        CmdDealDamage(health);
    }

   
    public void ApplyKnockBack(float knockBackAmount,Vector3 ProjectileDirection)
    {
        Debug.Log("ApplyKnockBack");
        knockback = health;
        EventOnDamageRecieved?.Invoke(knockBackAmount); //this will be where we will apply the KnockBack
        testVector = MathFunctions.calculateDirection(this.transform.position, ProjectileDirection);
        CmdKnockBack(health);
    }

    #endregion

    #region Client 
    [Command]// This is tp update the server Position
    void CmdUpdatePlayerPosition(Vector3 Location, Quaternion LocalRotation)
    {
        //This tells the servers where the player is and updates the position on the server
        RpcSyncPositionsClient(Location, LocalRotation);
    }

    IEnumerator StatusEffectCorutine(float time)
    {
        yield return new WaitForSeconds(time);
        CanShoot = true;
        RpcUnStun();

    }

    IEnumerator DamageEffectCorutine(float DamageAmount, float Tick)
    {
        yield return new WaitForSeconds(Tick);
    }

    #endregion

    //__________________________________

    #region Server 
    [ClientRpc]
    void RpcSyncPositionsClient(Vector3 Location, Quaternion LocalRotation)
    {
        //This tells all the cleints where this player pos it.
        if (isLocalPlayer)
            return;

        transform.position = Location;
        transform.localRotation = LocalRotation;
    }
    //Here we are going to add the playerClass when the player is spawned

    [Command]
    public void CmdDealDamage(float dmg)
    {
        this.health = dmg;
    }

    [Command]
    public void CmdKnockBack(float knockback)
    {
        this.knockback = knockback;
    }

    [Command]
    public void CmdChangeSpeed(float SpeedChange)
    {
        scaler = SpeedChange;
        setSpeed(SpeedChange);
    }

    [Command]
    public void CmdEffectTiming(float time)
    {
        StartCoroutine(StatusEffectCorutine(time));
    }

    //This will need to be changed to TargetRpc
    [ClientRpc]
    void RpcUnStun()
    {
        CanShoot = true;
    }

    #endregion


}
