using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityScript : MonoBehaviour
{

    [Header("Ability1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode ability1;

    [Header("Ability2")]
    public Image abilityImage2;
    public float cooldown2 = 10;
    bool isCooldown2 = false;
    public KeyCode ability2;



    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
    }

    void Ability1()
    {

        if (Input.GetKey(ability1) && isCooldown1 == false)
        {

            isCooldown1 = true;
            abilityImage1.fillAmount = 1;

        }

        if (isCooldown1)
        {

            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {

                abilityImage1.fillAmount = 0;
                isCooldown1 = false;

            }


        }


    }

    void Ability2()
    {

        if (Input.GetKey(ability2) && isCooldown2 == false)
        {

            isCooldown2 = true;
            abilityImage2.fillAmount = 1;

        }

        if (isCooldown2)
        {

            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {

                abilityImage2.fillAmount = 0;
                isCooldown2 = false;

            }


        }


    }






}

