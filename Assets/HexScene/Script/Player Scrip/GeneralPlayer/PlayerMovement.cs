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
    public int health;
    public int stamina;

    float scaler; // this is speed
    float knockback;
    bool isAlive;
    bool isReady;

    public Vector3 targetPoint;
    int NetworkId;

    private void Awake()
    {
        health = 100;
        stamina = 100;
    }

    void Start() {
        playerCameraGameObjectReferance = Instantiate<GameObject>(playerCameraGameObject, this.transform.position,Quaternion.identity) as GameObject;

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
    void Update () {
        
        if (!hasAuthority)
            return;

        if (isLocalPlayer) { 
            movement();
            faceMouse();
            CmdUpdatePlayerPosition(this.transform.position, this.transform.rotation);
        }
    }

    [Client]
    void movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            scaler = 2;
        }
        else
        {
            scaler = 1;
        }
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
            
            Debug.DrawLine(ray.origin, targetPoint,Color.red);
            //This does work But we need to give the player a new camera
           
        }

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

    [Command]// This is tp update the server Position
    void CmdUpdatePlayerPosition(Vector3 Location, Quaternion LocalRotation)
    {
        //This tells the servers where the player is and updates the position on the server
        RpcSyncPositionsClient(Location, LocalRotation);
    }

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
    
}
