using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //This may have to be changed to Scripable Objects
    Vector3 ProjectileDirection;
    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GetComponent<Transform>().position;
        ProjectileDirection = Input.mousePosition.normalized;
        MoveToMouse(ProjectileDirection);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToMouse(ProjectileDirection);
    }


    public void MoveToMouse(Vector3 Direction)
    {
        //Normalizing we get the distrance We can add a scaler on top of it.
        this.transform.Translate(new Vector3(Direction.x,0,Direction.y), Space.World);
    }

    //This will be where the ability has its effect
    public void AbilityEffect()
    {



    }

    private void OnCollisionEnter(Collision collision)
    {
        //destroy Self
        //Check what the FireBall Hit.
        
    }

    //Make a functioin to destroy after time.

}
