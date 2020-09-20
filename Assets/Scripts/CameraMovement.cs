using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    //https://answers.unity.com/questions/600577/camera-rotation-around-player-while-following.html
    InputActions inputActions;

    public float rotationSpeedInverse = 1.0f;

    public GameObject lookingAt;

    Vector2 mouseInput;

    Vector3 offeset;

    public float yUpperBound = 14.0f;
    public float yLowerBound = 4.0f;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Default.MouseInput.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        inputActions.Default.MouseInput.canceled += ctx => mouseInput = ctx.ReadValue<Vector2>();
        offeset = lookingAt.transform.position + new Vector3(10.0f, 10.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationSpeedInverse < 1.0f)
            rotationSpeedInverse = 1.0f;
        offeset = Quaternion.AngleAxis(mouseInput.x / rotationSpeedInverse, Vector3.up) * offeset;
        offeset = Quaternion.AngleAxis(mouseInput.y / rotationSpeedInverse, Vector3.forward) * offeset;

        if (offeset.y >= 14.0f)
            offeset.y = yUpperBound;
        else if (offeset.y <= 4.0f)
            offeset.y = yLowerBound;

        Debug.Log(offeset);

        transform.position = lookingAt.transform.position + offeset;
        transform.LookAt(lookingAt.transform);
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
