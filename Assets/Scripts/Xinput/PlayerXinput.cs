using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXinput : MonoBehaviour
{
    public XinputManager manager;
    public int playerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        manager.SetEventCallback(Button.A,Jump);
    }

    public void Jump(ControllerStickValues values){
        Debug.Log(values.leftStick.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
