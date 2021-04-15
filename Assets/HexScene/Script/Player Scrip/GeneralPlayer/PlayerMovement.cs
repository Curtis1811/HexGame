using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

using Mirror;



public class PlayerMovement : NetworkBehaviour
{
    public UnityEngine.Camera playerCamera;
    public GameObject playerCameraGameObject;
    public GameObject playerCameraGameObjectReferance;

    [Header("NetworkVariables")]
    //This Health is for Testing; CHANGE THESE TO PROTECTED AND USD FUNCTIONS TO CHANGE THEM
    [SyncVar]
    public float health;
    [SyncVar]
    public int stamina;
    [SyncVar]
    public float scaler; // this is speed
    public float OriginalScaler;
    float knockback;
    bool isAlive;
    bool isReady;
    [SyncVar]
    public bool CanShoot;


    [Header("Network Stuff")]    //We need to add a stun effect
    public Vector3 targetPoint;
    int NetworkId;

    //Test

    float x, y, z;

    //Here we are setting up delagets TESTING
    public delegate void SpeedChange(float SpeedAmount);
    public delegate void Stunned(bool stunned);
    public delegate void DamageRecieved(float Damage);

    [SyncEvent]
    public event SpeedChange EventOnSpeedChange;
    public event Stunned EventOnStunned;
    public event DamageRecieved EventOnDamageRecieved;

    Vector3 testVector;

    private void Awake()
    {
        health = 100;
        stamina = 100;
        knockback = 0;
        scaler = 1;
        OriginalScaler = 1;
        CanShoot = true;
    }

    void Start() {

        playerCameraGameObjectReferance = Instantiate<GameObject>(playerCameraGameObject, this.transform.position, Quaternion.identity) as GameObject;

        if (isLocalPlayer) {
            playerCameraGameObjectReferance.SetActive(true);
            playerCameraGameObjectReferance.GetComponent<CameraScript>().player = this.gameObject.transform;
            playerCamera = playerCameraGameObjectReferance.GetComponent<Camera>();
            //playerCamera.transform.rotation = new Vector3(90, 90, 0);
        }
        else
        {
            playerCameraGameObjectReferance.SetActive(false);
        }
    }

    //Update is called once per frame
    [Client]
    void Update() {

        if (!hasAuthority)
            return;

        if (isLocalPlayer) {
            if (CanShoot == true)
                movement();
            faceMouse();

            CmdUpdatePlayerPosition(this.transform.position, this.transform.rotation);
            
        }
    }

    [Client]
    void movement()
    {
        //sprint();
        //Here we have tom check and see if we need to apply KockBack; If this is the case we need to stop the player from moving while they are being knocked back.
        isBeingKnockedBack();
        this.transform.position += testVector* Time.deltaTime * knockback; //Need way more refining.

        if (Input.GetKey("w"))
        {
            this.transform.position += new Vector3(5, 0, 0) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("s"))
        {
            this.transform.position += new Vector3(-5, 0, 0) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("d"))
        {
            this.transform.position += new Vector3(0, 0, -5) * Time.deltaTime * scaler;
        }
        if (Input.GetKey("a"))
        {
            this.transform.position += new Vector3(0, 0, 5) * Time.deltaTime * scaler;
        }

    }
    public void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            scaler = OriginalScaler * 2;
        }
        else
        {
            scaler = OriginalScaler;
        }
    }
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
    void isBeingKnockedBack()
    {
        //Here is where we would starta  corutine
    }

    //Here we are doing to create some functions to get the mouse world Position Since all player objects will ahve a playmovements we can pass this to any class The player has
    //May have to move this to Different classes. 
    //WE are creating this here becasye we ahve a Face mouse Function that has data. 
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 TempVec = GetMouseWorldPositionWithZ(Input.mousePosition, UnityEngine.Camera.main);
        TempVec.y = 0;
        return TempVec;
    }
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

    public void setStun(bool Stunned, float time)
    {
        CanShoot = Stunned;
        EventOnStunned?.Invoke(CanShoot);
        CmdEffectTiming(time);

    }

    public void ApplyDamage(float Damage)
    {
        health += Damage;
        Debug.Log(Damage);
        EventOnDamageRecieved?.Invoke(Damage);
        CmdDealDamage(health);
    }

    public void ApplyKnockBack(float knockBackAmount,Vector3 ProjectileDirection)
    {
        Debug.Log("ApplyKnockBack");
        knockback += knockBackAmount;
        Debug.Log(MathFunctions.calculateDirection(this.transform.position, ProjectileDirection));
        EventOnDamageRecieved?.Invoke(knockBackAmount); //this will be where we will apply the KnockBack
        testVector =  MathFunctions.calculateDirection(this.transform.position, ProjectileDirection);
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
    public void CmdChangeSpeed(float SpeedChange)
    {
        scaler = SpeedChange;
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
