using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;
    public float height;
    public float SmoothSpeed = 0.25f;
    public float rotation;
    
	// Use this for initialization
	void Start () {

        //player = this.gameObject.GetComponent<GameObject>().transform;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.y = player.position.y + height;
        pos.z = player.position.z;

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, pos, SmoothSpeed * Time.deltaTime);
        this.transform.position = SmoothedPosition;
        this.transform.rotation = Quaternion.Euler(rotation,rotation,0);

    }

    public void cameraShake(){
        
    }
}
