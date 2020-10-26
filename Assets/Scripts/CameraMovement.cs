using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    InputActions _inputActions;

    public float rotationSpeedInverse = 1.0f;

    public GameObject lookingAt;
    public GameObject player;

    Vector2 _mouseInput;


    public float yUpperBound = 4.0f;
    public float yLowerBound = -3.0f;

    void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Default.MouseInput.performed += ctx => _mouseInput = ctx.ReadValue<Vector2>();
        _inputActions.Default.MouseInput.canceled += ctx => _mouseInput = ctx.ReadValue<Vector2>();

    }

    void Update()
    {
        if (rotationSpeedInverse < 1.0f)
            rotationSpeedInverse = 1.0f;

        //rotate camera on x z plane
        player.transform.rotation = Quaternion.AngleAxis(_mouseInput.x/ rotationSpeedInverse,Vector3.up) * player.transform.rotation;

        //increase/decrease height of camera target
        lookingAt.transform.position = lookingAt.transform.position + new Vector3(0.0f,_mouseInput.y/(rotationSpeedInverse*10.0f),0.0f);


        //clamp y position
        if (lookingAt.transform.localPosition.y > yUpperBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yUpperBound, lookingAt.transform.localPosition.z);
        else if(lookingAt.transform.localPosition.y < yLowerBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yLowerBound, lookingAt.transform.localPosition.z);

        //lock on to target
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
