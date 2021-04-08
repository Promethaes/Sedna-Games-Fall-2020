using UnityEngine;
using System.Collections;
public class Secrets 
{
    private static Secrets instance;
    private Secrets() { }
    public static Secrets Instance()
    {
       if(instance == null)
        {
            instance = new Secrets();
        }
        return instance;
    }
    public static Vector3 limitKnockBack(Vector3 force)
    {
        float forceLimit = 5f;
        if (NoKnockbackLimit)
            return force;
        else
            return new Vector3(Mathf.Clamp(force.x,-forceLimit, forceLimit), Mathf.Clamp(force.y, -forceLimit, forceLimit), Mathf.Clamp(force.z, -forceLimit, forceLimit))
;   }


    public static bool FlyingBison = false;
    public static bool NoKnockbackLimit = false;

}
