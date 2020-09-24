using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    InputActions _inputActions;

    public float moveSpeed = 1.0f;

    Vector2 moveInput;

    float _isSprinting = 0.0f;

    void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Default.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Default.Movement.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Default.Sprint.performed += ctx => _isSprinting = ctx.ReadValue<float>();
        _inputActions.Default.Sprint.canceled += ctx => _isSprinting = ctx.ReadValue<float>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(moveInput.x,0.0f, moveInput.y) * moveSpeed;

        if (_isSprinting > 0.0f)
            Debug.Log("sprint");
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }
}
