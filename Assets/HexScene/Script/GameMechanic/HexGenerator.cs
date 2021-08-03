using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HexGenerator : NetworkBehaviour
{
    [SerializeField] GameObject hexPrefab;
    [SerializeField] float CircleRadius;
    [SerializeField] List<GameObject> hex;
    [SerializeField] float hexRadius;
    float RotationAngle;
    Vector3 startingVector;
    float hexCount;
    

   // Start is called before the first frame update   
   void Start()
    {
        hexPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        CircleRadius = 10;
        hexRadius = 5f; // 3.2 basevalue
        RotationAngle = 1f;
        startingVector = new Vector3(hexRadius, 0, hexRadius);//hexRadius);
        // this is 60 deg in radions
        // THis is how we find out X Mathf.Cos(1);
        // this is how we find out y Mathf.Sin(1);
        spawningHex();
        Instantiate<GameObject>(hexPrefab, new Vector3(0,0,0), Quaternion.identity);

        Debug.Log("HexCount: " + hexCount);

    }

    //k Circle Radius,

    // Update is called once per frame
    void Update()
    {
         
    }

    // sk = 3Ksqr + 3k + 1 this equals the amount of  
   
    void spawningHex()
    {
        // everytime there is new "spawn Circle the Angle Halfs"
        hexRadius = 0;
        float CurrentAngleRotation = 1f;
        float FlatValue;
        // we need to get the vector direciton and change it by 30 each time we spawn a new hex. we are then rotating the 
        for (int k = 0; k < CircleRadius; k++)// here is essentially the number of circles
        {

            hexCount = (k * k) * 3 + 3 * k;
            for (int x = 0; x < hexCount; x++)
            {
                              
                GameObject Temp = Instantiate<GameObject>(hexPrefab, calculateSpawnPoint(CurrentAngleRotation, startingVector, hexRadius), Quaternion.identity);
                CurrentAngleRotation += RotationAngle;
                Debug.Log(calculateSpawnPoint(CurrentAngleRotation, startingVector,hexRadius)); 
            }
            //CurrentAngleRotation = CurrentAngleRotation / 2;
            hexRadius += 2.8f;
            Debug.Log(startingVector + "HexCounter");
            
        }
            //new Vector3(3.2f*2,0,0));
    }
     
   
     
    public Vector3 calculateSpawnPoint(float angle,Vector3 parsedVector,float Scaler)
    {
        float PVM = Vector3.Magnitude(parsedVector);
        PVM = Mathf.Sqrt(PVM);
        Vector3 temp = parsedVector;
        temp.x = PVM * Mathf.Cos(angle);
        temp.z = PVM * Mathf.Sin(angle);
        temp.y = 0;
        temp = temp * Scaler;
        return temp;
    } 


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawLine(this.transform.position, new Vector3(3.2f,0,0));
        //Gizmos.DrawSphere(new Vector3(0,0,0), CircleRadius);
    }



}
