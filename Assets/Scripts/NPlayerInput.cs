using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPlayerInput : MonoBehaviour
{

    Vector2 _moveInput;
    Vector2 _mouseInput;
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float dashSpeed = 20.0f;

    float _dashCooldown = 0.0f;
    float _isDashing = 0.0f;
    bool _dashed = false;

    bool _airDashed = false;

    float _jumpCooldown = 0.0f;
    float _isJumping = 0.0f;
    bool _jumped = false;
    bool _doubleJumped = false;

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

        _jumpCooldown -= Time.deltaTime;
        _dashCooldown -= Time.deltaTime;

        //Jump Movement
        if (_isJumping == 1.0f)
            _Jump();

        //Dash Movement
        if (_isDashing == 1.0f)
            _Dash();
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
        float y = player.GetComponent<Rigidbody>().velocity.y;
        Vector3 vel = Quaternion.AngleAxis(270, Vector3.up) * ((Quaternion.AngleAxis(180, Vector3.up) * (transform.forward * _moveInput.x)) + (Quaternion.AngleAxis(90, Vector3.up) * (transform.forward * _moveInput.y))) * moveSpeed;
        //NOTE: Dashing ignores camera direction when applied
        if (_dashCooldown > 0.0f)
            player.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x, y, player.GetComponent<Rigidbody>().velocity.z);
        else
            player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, y, vel.z);
    }

    void _Jump()
    {
        //TODO: Replace jump cooldown with a check for floor collision
        if (_jumpCooldown < 0.0f)
        {
            _jumped = false;
            _doubleJumped = false;
        }
        if (_jumpCooldown < 1.8f && _jumped && !_doubleJumped)
        {
            player.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, _isJumping, 0.0f) * jumpSpeed;
            _doubleJumped = true;
            _jumped = false;
            _jumpCooldown = 2.5f;
        }
        if (_jumpCooldown < 0.0f && !_jumped)
        {
            player.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, _isJumping, 0.0f) * jumpSpeed;
            _jumped = true;
            _jumpCooldown = 2.5f;
        }
    }

    void _Dash()
    {
        if (_dashed && !_airDashed && _jumped)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x * dashSpeed, vel.y, vel.z * dashSpeed);
            _airDashed = true;
            //[TODO] Replace cooldown with a check on touching ground for air dashing
            _dashCooldown = 1.5f;
        }
        if (_dashCooldown < 0.0f && !_dashed)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x * dashSpeed, vel.y, vel.z * dashSpeed);
            _dashed = true;
            //_dashCooldown represents duration of dash
            _dashCooldown = 1.5f;
        }
        if (_dashCooldown < 0.0f)
        {
            _dashed = false;
            _airDashed = false;
        }
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnMouseInput(InputAction.CallbackContext ctx)
    {
        _mouseInput = ctx.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        _isJumping = ctx.ReadValue<float>();
    }
    public void OnDash(InputAction.CallbackContext ctx)
    {
        _isDashing = ctx.ReadValue<float>();
    }

}
