using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    InputActions _inputActions;

    public float rotationSpeedInverse = 1.0f;

    public GameObject lookingAt;

    Vector2 _mouseInput;


    public float yUpperBound = 14.0f;
    public float yLowerBound = 4.0f;

    void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Default.MouseInput.performed += ctx => _mouseInput = ctx.ReadValue<Vector2>();
        _inputActions.Default.MouseInput.canceled += ctx => _mouseInput = ctx.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rotationSpeedInverse < 1.0f)
            rotationSpeedInverse = 1.0f;

        lookingAt.transform.rotation = lookingAt.transform.rotation * Quaternion.Euler(0.0f, _mouseInput.x / rotationSpeedInverse, 0.0f);
        lookingAt.transform.rotation = lookingAt.transform.rotation * Quaternion.Euler(_mouseInput.y / rotationSpeedInverse, 0.0f, 0.0f);

        transform.LookAt(lookingAt.transform);
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
