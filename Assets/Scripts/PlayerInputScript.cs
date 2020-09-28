
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    InputActions inputActions;
    public float moveSpeed;
    public float jumpSpeed;
    float _dashCooldown = 0.0f;
    float _isDashing = 0.0f;
    bool _dashed = false;
    bool _airDashed = false;
    float _jumpCooldown = 0.0f;
    float _isJumping = 0.0f;
    bool _jumped = false;
    bool _doubleJumped = false;
    Vector2 _moveInput = new Vector2(0.0f,0.0f);

    float _isSprinting = 0.0f;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Default.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        inputActions.Default.Movement.canceled += ctx => _moveInput = ctx.ReadValue<Vector2>();

        inputActions.Default.Jump.performed += ctx => _isJumping = ctx.ReadValue<float>();
        inputActions.Default.Jump.canceled += ctx => _isJumping = ctx.ReadValue<float>();

        inputActions.Default.Dash.performed += ctx => _isDashing = ctx.ReadValue<float>();
        inputActions.Default.Dash.canceled += ctx => _isDashing = ctx.ReadValue<float>();
    }


    // Update is called once per frame
    void Update()
    {
        //WASD Movement
        Move(_moveInput*Time.deltaTime);

        _jumpCooldown -= Time.deltaTime;
        _dashCooldown -= Time.deltaTime;

        //Jump Movement
        if (_isJumping == 1.0f)
        Jump();

        //Dash Movement
        if (_isDashing == 1.0f)
        Dash();
    }

    void Move(Vector2 _moveInput)
    {
        float y = gameObject.GetComponent<Rigidbody>().velocity.y;
        Vector3 vel = Quaternion.AngleAxis(270, Vector3.up)*((Quaternion.AngleAxis(180, Vector3.up)*(transform.forward*_moveInput.x)) + (Quaternion.AngleAxis(90, Vector3.up)*(transform.forward*_moveInput.y)))*moveSpeed;
        //NOTE: Dashing ignores camera direction when applied
        if (_dashCooldown > 0.0f)
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, y, gameObject.GetComponent<Rigidbody>().velocity.z);
        else
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, y, vel.z);
    }

    void Jump()
    {
        //TODO: Replace jump cooldown with a check for floor collision
        if (_jumpCooldown < 0.0f)
        {
            _jumped = false;
            _doubleJumped = false;
        }
        if (_jumpCooldown < 1.8f && _jumped && !_doubleJumped)
        {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, _isJumping, 0.0f) * jumpSpeed;
        _doubleJumped = true;
        _jumped = false;
        _jumpCooldown = 2.5f;
        }
        if (_jumpCooldown < 0.0f && !_jumped)
        {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, _isJumping, 0.0f) * jumpSpeed;
        _jumped = true;
        _jumpCooldown = 2.5f;
        }
    }
    void Dash()
    {
        if (_dashed && !_airDashed && _jumped)
        {
        Vector3 vel = gameObject.GetComponent<Rigidbody>().velocity;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(vel.x*2.0f, vel.y, vel.z*2.0f);
        _airDashed = true;
        //[TODO] Replace cooldown with a check on touching ground for air dashing
        _dashCooldown = 1.5f;
        }
        if (_dashCooldown < 0.0f && !_dashed)
        {
        Vector3 vel = gameObject.GetComponent<Rigidbody>().velocity;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(vel.x*2.0f, vel.y, vel.z*2.0f);
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

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }
}
