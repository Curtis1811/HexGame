using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    //Here is where we are going to handle all the data that our Cooldown Had
    [SerializeField] public List<CooldownData> cooldown = new List<CooldownData>();

    //In here wea re going to have some kind of Charge System.

    public void Update()
    {
        ProceessCooldown();
    }

    public void ProceessCooldown()
    {
        float deltatime = Time.deltaTime;
        for (int i = cooldown.Count - 1; i >= 0; i--)
        {
            if (cooldown[i].DecreaseCooldown(deltatime))
            {
                cooldown.RemoveAt(i);
            }
        }
    }

    public bool isOnCooldown(int id)
    {
        foreach(CooldownData cooldown in cooldown)
        {
            if (cooldown.id == id)
                return true;
        }
        return false;
    }

    public float getRemaningDuration(int id)
    {
        foreach (CooldownData cooldown in cooldown)
        {
            if (cooldown.id != id)
                continue;
            return cooldown.remaningTime;
        }
        //This means that there is no Cooldown on the ability.
        return 0f;
    }

    public void PutOnCooldown(ICooldownInterface HadCooldown)
    {
        cooldown.Add(new CooldownData(HadCooldown));
    }


}

public class CooldownData{
    public int id { get; }
    public float remaningTime { get; private set; }
    public CooldownData(ICooldownInterface cooldown)
    {
        id = cooldown.id;
        remaningTime = cooldown.CooldownDuration;
    }

    public bool DecreaseCooldown(float DeltaTime)
    {
        remaningTime = Mathf.Max(remaningTime - DeltaTime, 0f);

        return remaningTime == 0f;
    }
}
