using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundDissapering : MonoBehaviour
{
    //GameObject[] groundObjects;

    List<GameObject> hex;
    GameObject[] SolidGround;
    //List<int> RemovedList;

    int storedRand;
    

    // Use this for initialization
  
    void Start()
    {
        //groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        hex = GameObject.FindGameObjectsWithTag("Ground").ToList<GameObject>();
        StartCoroutine("hexFunc");
        SolidGroundColor();


    }

    // Update is called once per frame
    
    void Update()
    {
        Debug.Log("Hex Count " + hex.Count);
       

    }

    public IEnumerator hexFunc()
    {
        while (hex.Count >= 0)
        {
           

            if (hex.Count > 0)
            {
            int rand;
                
                rand = Random.Range(0, hex.Count);
                storedRand = rand;
                changeColor();
                
                yield return new WaitForSeconds(1f);

                
                hex[rand].gameObject.GetComponentInChildren<Collider>().enabled = false;
                hex[rand].gameObject.GetComponentInChildren<Renderer>().enabled = false;

                hex.RemoveAt(rand);

                if (hex.Count <= 0)
                {
                    Debug.Log("Break");
                    break;
                    
                }

            }
        }


        void changeColor()
        {

            if (hex[storedRand].GetComponentInChildren<Renderer>().material.color != Color.red)
            {
               hex[storedRand].GetComponentInChildren<Renderer>().material.color = Color.red;
                
            }
           
        }
    }

    void SolidGroundColor()
    {
        int count = 0;
        SolidGround = GameObject.FindGameObjectsWithTag("SolidGround");

        foreach (GameObject GO in SolidGround)
        {
            count++;
            SolidGround[count-1].GetComponentInChildren<Renderer>().material.color = Color.blue;
        }
    }
}

