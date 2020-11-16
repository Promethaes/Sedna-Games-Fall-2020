﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public enum Button
{
    DPAD_UP = 1,
    DPAD_DOWN = 2,
    DPAD_LEFT = 4,
    DPAD_RIGHT = 8,

    START = 16,
    BACK = 32,

    THUMB_LEFT = 64,
    THUMB_RIGHT = 128,

    SHOULDER_LEFT = 256,
    SHOULDER_RIGHT = 512,

    A = 4096,
    B = 8192,
    X = 16384,
    Y = 32768
}

public struct ControllerStickValues
{
    public UnityEngine.Vector2 leftStick;
    public UnityEngine.Vector2 rightStick;
}

public class XinputManager : MonoBehaviour
{
    const string DllName = "XinputDLL";
    private Dictionary<Button, Callback> callbackList = new Dictionary<Button, Callback>();

    [DllImport(DllName)]
    private static extern void UpdateGamepadList();

    [DllImport(DllName)]
    private static extern bool GetEventValue(int playerIndex, int e);

    [DllImport(DllName)]
    private static extern float GetLeftStickValueXORY(int playerIndex,bool xOrY);

    [DllImport(DllName)]
    private static extern float GetRightStickValueXORY(int playerIndex, bool xOrY);
    // Start is called before the first frame update
    void Start()
    {
    }

    public delegate void Callback(ControllerStickValues values);
    public delegate void StickCallback(ControllerStickValues values);

    public void SetEventCallback(Button button, Callback callback)
    {
        callbackList[button] = callback;
    }

    StickCallback _leftStickCallback;
    StickCallback _rightStickCallback;
    public void SetLeftStickCallback(StickCallback callback)
    {
        _leftStickCallback = callback;
    }

    public void SetRightStickCallback(StickCallback callback)
    {
        _rightStickCallback = callback;
    }


    private void CallTheCallbacks()
    {
        for (int i = 0; i < 4; i++)
        {
            ControllerStickValues values = new ControllerStickValues();
          
            values.leftStick.x = GetLeftStickValueXORY(i,false);
            values.leftStick.y = GetLeftStickValueXORY(i,true);
            values.rightStick.x = GetRightStickValueXORY(i,false);
            values.rightStick.y = GetRightStickValueXORY(i,true);

            if (values.leftStick.magnitude > 0.0 && _leftStickCallback != null)
                _leftStickCallback.Invoke(values);
            if (values.rightStick.magnitude > 0.0 && _rightStickCallback != null)
                _rightStickCallback.Invoke(values);

            for (int j = 1; j < (int)Button.Y; j += j)
            {
                Button b = (Button)j;

                if (GetEventValue(i, (int)b) && callbackList.ContainsKey(b))
                    callbackList[b].Invoke(values);

                if (i == 512)
                    i = 2048;

            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateGamepadList();
        CallTheCallbacks();
    }
}
