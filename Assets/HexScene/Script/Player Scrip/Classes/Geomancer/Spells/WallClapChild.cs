using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WallClapChild : NetworkBehaviour
{
    //Here wea re going to define a delegate;

    //Here is where we are calling the deletate
    public delegate void colliding(GameObject gameObject);
    public colliding onColliding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other) {
        //We are going to call a delagete 
        if(other.tag == "Player"){
            //Debug.Log(MathFunctions.CalculateDotProcuct(Vector3.Normalize(other.transform.position), Vector3.Normalize(this.transform.position)));
        }
        onColliding?.Invoke(other.gameObject);
       
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other){
        
        if(other.tag == "Player"){
            onColliding?.Invoke(other.gameObject);
        }

    }
}
