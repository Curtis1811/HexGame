using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyromancer : PlayerMovement
{
    // Start is called before the first frame update
    public Vector3 mouseVector;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicAttack();
    }

    void BasicAttack()
    {
        mouseVector = Input.mousePosition;
        
        
    }

}
