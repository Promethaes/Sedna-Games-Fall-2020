using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XinputGamepad : MonoBehaviour
{
    public Dictionary<Button, XinputManager.Callback> callbackList = new Dictionary<Button, XinputManager.Callback>();

    public int index = 0;

    public Vector2 leftStick = new Vector2();
    public Vector2 rightStick = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        var manager = FindObjectOfType<XinputManager>();
        manager.gamepads.Add(this);
        index = manager.gamepads.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEventCallback(Button button, XinputManager.Callback callback)
    {
        callbackList[button] = callback;
    }

    public XinputManager.StickCallback _leftStickCallback;
    public XinputManager.StickCallback _rightStickCallback;

    public void SetLeftStickCallback(XinputManager.StickCallback callback)
    {
        _leftStickCallback = callback;
    }

    public void SetRightStickCallback(XinputManager.StickCallback callback)
    {
        _rightStickCallback = callback;
    }

}
