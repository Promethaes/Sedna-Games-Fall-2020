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

    bool _useAbility = false;
    public CharMenuInput charMenuInput;
    public int playerType;
    public bool insideCastingZone = false;
    public RamThroughScript ramThrough;
    public BubbleShieldScript bubbleShieldScript;
    public PolarBearScript polarBearScript;

    bool _attack = false;
    float _comboDuration = 0.0f;
    float _animationDuration = 0.5f;
    int _comboCounter = 0;
    float[] _damageValues = new float[3];
    float[] _animationDelay = new float[3];

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
        playerType = charMenuInput.playerType;

        _setCombo(10.0f, 15.0f, 20.0f, 0.35f, 0.75f, 1.10f);

    }

    // Update is called once per frame
    void Update()
    {
        _MouseInput();
        _Move();

        _jumpCooldown -= Time.deltaTime;
        _dashCooldown -= Time.deltaTime;
        _comboDuration -= Time.deltaTime;
        _animationDuration -= Time.deltaTime;

        //Jump Movement
        if (_isJumping == 1.0f)
            _Jump();

        //Dash Movement
        if (_isDashing == 1.0f)
            _Dash();

        if (insideCastingZone)
            _UseAbility();

        if (_attack)
            _Attack();
    }


    void _UseAbility()
    {
        //activate some prompt

        if (!_useAbility)
            return;


        //add more...wait i dont think we need to add more than one lmaooooooooooooooooooooooooo
        if (playerType == 1)
            bubbleShieldScript.AttemptToCast();
        else if (playerType == 3)
            polarBearScript.Transition();
        else if (playerType == 4)
        {
            ramThrough.hasRammed = true;
        }
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

    void _setCombo(float x, float y, float z, float u, float v, float w)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        _damageValues[0] = x;
        _damageValues[1] = y;
        _damageValues[2] = z;
        _animationDelay[0] = u;
        _animationDelay[1] = v;
        _animationDelay[2] = w;
    }


    void _Attack()
    {
        if (_animationDuration < 0.0f)
        {
            _animationDuration = _animationDelay[_comboCounter];
            if (_comboDuration < 0.0f)
                _comboCounter = 0;
            //TODO: Animation
            RaycastHit enemy;
            if (Physics.Raycast(transform.position, transform.forward, out enemy, 2.0f) && enemy.transform.tag == "Enemy")
            {
                Enemy foe = enemy.collider.GetComponent<Enemy>();
                foe.takeDamage(_damageValues[_comboCounter]);
                Debug.Log(foe.getHealth());
            }
            _comboCounter++;
            if (_comboCounter > 2)
                _comboCounter = 0;
            _comboDuration = 2.0f;
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

    public void OnAbility(InputAction.CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _useAbility = true;
        else
            _useAbility = false;
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _attack = true;
        else
            _attack = false;

    }


}
