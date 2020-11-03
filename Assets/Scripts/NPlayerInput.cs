using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPlayerInput : MonoBehaviour
{

    Vector2 _mouseInput;
    //NOTE: _mouseSpeed changes mouse sensitivity. Implement into options in the future
    float _mouseSpeed = 1.2f;
    Vector2 _moveInput;
    public float moveSpeed = 10.0f;
    public float jumpSpeed = 5.0f;
    public float hopSpeed = 0.25f;
    public float dashSpeed = 2.5f;
    bool _isGrounded = true;
    RaycastHit terrain;

    float _dashDuration = 0.0f;
    float _dashCooldown = 0.0f;
    float _isDashing = 0.0f;
    bool _dashed = false;

    bool _airDashed = false;

    float _jumpAnimDuration = 0.0f;
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
        _comboDuration -= Time.deltaTime;
        _animationDuration -= Time.deltaTime;


        if (insideCastingZone)
            _UseAbility();

        if (_attack)
            _Attack();
    }

    //Physics update (FixedUpdate); updates at set intervals
    private void FixedUpdate() 
    {
        _jumpAnimDuration -= Time.fixedDeltaTime;
        _dashDuration -= Time.fixedDeltaTime;
        _dashCooldown -= Time.fixedDeltaTime;

        //Jump Movement
        if (_isJumping == 1.0f)
            _Jump();

        //Dash Movement
        if (_isDashing == 1.0f)
            _Dash();
    }

    //Called after physics (FixedUpdate); used to prevent sliding on slopes due to high gravity
    private void LateUpdate() 
    {
        _MouseInput();
        //TODO: Figure out why sliding happens regardless of y being updated in _Move() in LateUpdate()
        _Move();
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
        player.transform.rotation = Quaternion.AngleAxis(_mouseInput.x / rotationSpeedInverse * _mouseSpeed, Vector3.up) * player.transform.rotation;

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
        if (_dashDuration < 0.0f)
        {
            //NOTE: Camera position affects the rotation of the player's movement, which is stored in the first value of Vector3 vel (Current: 135.0f)
            Vector3 vel = Quaternion.AngleAxis(135.0f, Vector3.up) * ((Quaternion.AngleAxis(180, Vector3.up) * (transform.forward * _moveInput.x)) + (Quaternion.AngleAxis(90, Vector3.up) * (transform.forward * _moveInput.y)));
            vel *= moveSpeed;

            float y = player.GetComponent<Rigidbody>().velocity.y;
            //NOTE: Checks for _isGrounded to reduce the effects of gravity such that the player doesn't slide off slopes
            //TODO: Adjust raycast for actual models' radii
            //NOTE: Raycasts downwards for terrain collision, checking at a distance of 0.6f (0.5f radius, 0.1f actual check)
            _isGrounded = Physics.Raycast(transform.position, -transform.up, out terrain, 0.6f);
            if (_isGrounded && terrain.transform.tag == "Terrain")
            {
                y = -1.0f;

                //NOTE: Updates the state of dashes and jumps
                _dashed = false;
                _airDashed = false;
                if (_jumpAnimDuration <= 0.0f)
                {
                    _jumped = false;
                    _doubleJumped = false;
                }
            }

            player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, y, vel.z);
        }
    }

    void _Jump()
    {
        //NOTE: Resets the button so that the player doesn't accidentally double jump
         _isJumping = 0.0f;
        //NOTE: Jumping adds a slight boost to the x/z direction you move in to simulate push back against the ground
        //NOTE: Gravity is set to -24.525f. To change it: Edit -> Project Settings -> Physics -> y = newGravityValue
        float jump = Mathf.Sqrt(jumpSpeed * -2.0f * -24.525f);
        
        if (_jumpAnimDuration <= 0.0f && _isGrounded && !_jumped)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _jumped = true;
            _jumpAnimDuration = 0.3f;
        }
        else if (_jumpAnimDuration <= 0.0f && !_doubleJumped)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            //NOTE: Adjusts double jump to give full jump height rather than being affected by gravity. Remove if necessary
            player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, 0.0f, vel.z);
            player.GetComponent<Rigidbody>().AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _doubleJumped = true;
            _jumpAnimDuration = 0.3f;
        }
    }

    void _Dash()
    {
        //NOTE: Resets the button so that the player doesn't accidentally dash + air dash while holding the button
         _isDashing = 0.0f;

        //NOTE: _dashCooldown stops dash chaining that would lead to acceleration
        if (_dashCooldown <= 0.0f && _dashDuration <= 0.0f && !_dashed && _isGrounded)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
            _dashed = true;
            _dashDuration = 0.35f;
            _dashCooldown = 0.5f;
        }
        else if (_dashCooldown <= 0.0f && _dashDuration <= 0.0f && !_airDashed)
        {
            Vector3 vel = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
            _airDashed = true;
            _dashDuration = 0.35f;
            _dashCooldown = 0.5f;
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
