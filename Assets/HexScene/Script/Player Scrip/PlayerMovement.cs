using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
//using Mirror;
using System.Runtime.InteropServices;

public class PlayerMovement : MonoBehaviour // NetworkBehaviour
{
    public Camera playerCamera;

    float scaler;

    void Start() {
        //playerCamera = Camera.main;
    }
	
	// Update is called once per frame
	//[Client]
    void Update () {

        //if (!hasAuthority)
           // return;

        movement();
        faceMouse();

	}

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
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        //When Using Camera If you have a camera scrip you Must use unity engine infront of it

        float hitDistance = 0f;
        if (PlayerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,7f * Time.deltaTime);
        }

    }

}
