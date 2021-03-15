using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;
    public float height;

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
        this.transform.position = pos;
        this.transform.rotation = Quaternion.Euler(90,90,0);
        //this.transform.rotation

    }
}
