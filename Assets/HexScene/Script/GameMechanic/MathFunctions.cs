using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathFunctions 
{
    // Start is called before the first frame update
   public static Vector3 calculateDirection(Vector3 CurrentPosition, Vector3 TargetPosition) {

        Vector3 temp = new Vector3(0, 0, 0);
        Vector3 temp1 = new Vector3(0, 0, 0);
        temp = CurrentPosition- TargetPosition;
        float tempFloat = Vector3.Magnitude(temp);
        temp = temp / tempFloat;
        //First we are going to get the direction of the vector.
        return temp;
    }

    public static Vector3 CalculateKnockBackDirection(Vector3 player, Vector3 Projectile)
    {
        //This Functions is for basic Projectiles
        return new Vector3(0,0,0);
    }
    public static Vector2 CalculateKnockBackDirection(Vector2 player, Vector2 Projectile)
    {
        //This Functions is for basic Projectiles
        return new Vector3(0,0,0);
    }


    public static float KnockBackAmout()
    {
        // Here is where we are going to work out the knock back amount and decay 
        return 0f;
    }

    public static float CalculateDotProcuct(Vector3 a, Vector3 b){

        float result = 0f;
        float xData = a.x*b.x;
        float yData = a.y*b.y;
        result = xData+yData;
        
        return result;
    }
    public static float CalculateDotProcuct(Vector2 a, Vector2 b){

        float result = 0f;
        
        return result;
    }

}
