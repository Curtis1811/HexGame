using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    //update is called once per frame


    [ServerCallback]
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("ok");
            ClientScene.UnregisterPrefab(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    

}
