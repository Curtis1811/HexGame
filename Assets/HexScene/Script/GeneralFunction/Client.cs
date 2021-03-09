using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Client : NetworkBehaviour
{
    // Start is called before the first frame update
    PlayerData pd;
    void Start()
    {
        pd = SaveData.loadData();
       
        if (isLocalPlayer)
        OnPointerCharacterClassSelector();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerCharacterClassSelector()
    {
        switch (pd.Class)
        {
            case 1:
                Debug.Log("Pyromancer");
                //PyromancerHandler pro = new PyromancerHandler(tempString);
                this.gameObject.AddComponent<PyromancerHandler>();
                this.GetComponent<PyromancerHandler>().abilityData = pd.SaveAbilites;//.abilityData = tempString;
                //player.GetComponent<Pyromancer>().AbilityNames = tempString;
                break;

            case 2:
                Debug.Log("New Class Added Hydromancer");
                break;

            case 3:
                Debug.Log("New Class Added Aeromancer");
                break;

            case 4:
                Debug.Log("New Class Added Geomancer");
                break;

            default:
                Debug.Log("NoClass");
                break;
        }
    }

}
