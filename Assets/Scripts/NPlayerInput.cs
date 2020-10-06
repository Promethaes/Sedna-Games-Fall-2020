using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPlayerInput : MonoBehaviour
{

    Vector2 _moveInput;
    Vector2 _mouseInput;
    public float moveSpeed = 5.0f;

    //Camera
    public GameObject pCamera;
    public GameObject player;
    public GameObject lookingAt;
    public float yUpperBound = 4.0f;
    public float yLowerBound = -3.0f;
    public float rotationSpeedInverse = 1.0f;
    //End of Camera

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _MouseInput();
        _Move();
    }

    void _MouseInput()
    {
        if (rotationSpeedInverse < 1.0f)
            rotationSpeedInverse = 1.0f;

        //rotate camera on x z plane
        player.transform.rotation = Quaternion.AngleAxis(_mouseInput.x / rotationSpeedInverse, Vector3.up) * player.transform.rotation;

        //increase/decrease height of camera target
        lookingAt.transform.position = lookingAt.transform.position + new Vector3(0.0f, _mouseInput.y / (rotationSpeedInverse * 10.0f), 0.0f);


        //clamp y position
        if (lookingAt.transform.localPosition.y > yUpperBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yUpperBound, lookingAt.transform.localPosition.z);
        else if (lookingAt.transform.localPosition.y < yLowerBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yLowerBound, lookingAt.transform.localPosition.z);

        //lock on to target
        pCamera.transform.LookAt(lookingAt.transform);
    }

    void _Move()
    {
        float y = gameObject.GetComponent<Rigidbody>().velocity.y;
        Vector3 vel = Quaternion.AngleAxis(270, Vector3.up) * ((Quaternion.AngleAxis(180, Vector3.up) * (transform.forward * _moveInput.x)) + (Quaternion.AngleAxis(90, Vector3.up) * (transform.forward * _moveInput.y))) * moveSpeed;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, y, vel.z);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnMouseInput(InputAction.CallbackContext ctx)
    {
        _mouseInput = ctx.ReadValue<Vector2>();
    }

}
