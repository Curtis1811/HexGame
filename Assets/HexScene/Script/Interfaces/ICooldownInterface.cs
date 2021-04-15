using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldownInterface 
{
    // Start is called before the first frame update
    int id { get; }// This will only be used to get the ID not to Set the ID
    float CooldownDuration { get; }

}
