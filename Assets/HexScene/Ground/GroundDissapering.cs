using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundDissapering : MonoBehaviour {
    GameObject[] groundObjects;

    List<GameObject> hex;
    //List<int> RemovedList;

    int storedRand;
    int hexCount;
    // Use this for initialization
    void Start () {
        groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        hex = GameObject.FindGameObjectsWithTag("Ground").ToList<GameObject>();
        hexCount = hex.Count;
        Debug.Log(hexCount);
	}
	
	// Update is called once per frame
	void Update () {

        if (hexCount<=0)
        {
            StopCoroutine("hexFunc");
        
        }else{
           
            StartCoroutine("hexFunc");
        
        }
        
    }

    IEnumerator hexFunc()
    {
        changeColor();
        Debug.Log(hexCount);

        if (hexCount > 0)
        {

            int rand = Random.Range(0, hex.Count);
            storedRand = rand;

            
           // RemovedList.Add(storedRand);

           hex.Remove(hex[storedRand]);
           hex[storedRand].gameObject.GetComponent<Collider>().enabled = false;
           //hex[storedRand].gameObject.GetComponentInChildren<Renderer>().enabled = false;
           //GetComponent<Mesh>().enabled = false;


            hexCount--;

        }
        yield return new WaitForSeconds(1.8f);

        Debug.Log(hexCount);
    }


    void changeColor()
    {
       
        if (hex[storedRand].GetComponent<Renderer>().material.color == Color.red)
        {
            return;
        }
        else
        {
            hex[storedRand].GetComponent<Renderer>().material.color = Color.red;
            
            storedRand = 0;
        }
    }
}
