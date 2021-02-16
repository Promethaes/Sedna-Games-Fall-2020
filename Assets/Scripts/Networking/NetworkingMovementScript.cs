﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkingMovementScript : MonoBehaviour
{

    Vector3 moveVec = new Vector3();
    Rigidbody rigidbody = null;

    public int networkedPlayerNum = -1;
    public float movespeed = 10.0f;
    public float jumpheight = 5.0f;
    public bool readyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    float timer = 0.033f;
    float mTimer = 0.033f;
    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity + moveVec;
        moveVec.y = 0;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        var temp = ctx.ReadValue<Vector2>();
        moveVec = new Vector3(temp.x, 0.0f, temp.y) * movespeed;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        var temp = ctx.ReadValue<float>();

        if (temp > 0.5f)
            moveVec.y = jumpheight;
    }

    public void OnReady(InputAction.CallbackContext ctx)
    {
        var temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            Debug.Log(gameObject.name + " Is Ready");
    }
}
