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
        manager.SetLeftStickCallback(Move);

    }

    public void Move(ControllerStickValues values)
    {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(values.leftStick.x, 0.0f, values.leftStick.y) * 10.0f * Time.deltaTime;
    }

    public void Jump(ControllerStickValues values)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 10.0f, 0.0f) * 10.0f * Time.deltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
