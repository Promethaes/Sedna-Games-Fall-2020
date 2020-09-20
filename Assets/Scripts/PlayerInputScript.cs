using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    InputActions inputActions;

    public float moveSpeed = 1.0f;

    Vector2 moveInput;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Default.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Default.Movement.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(moveInput.x,0.0f, moveInput.y) * moveSpeed;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
}
