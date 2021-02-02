using System;
using System.Collections;
using UnityEngine;
using Unity;



public class PlayerController : MonoBehaviour
{

    [Header("Player variables")]
    [SerializeField] private float moveSpeed = 12.0f;
    [SerializeField] private float jumpSpeed = 3.0f;
    [SerializeField] private float dashSpeed = 2.5f;
    [SerializeField] private float attackDistance = 8.0f;

    [Header("Player abilities")]
    // Default mesh is turtle, if a turtle has bison abilities, either this or the player mesh in GameInputHandler did not get set properly
    public PlayerType playerType = PlayerType.BISON;
    [SerializeField] private RamThroughScript ramThrough = null;
    [SerializeField] public BubbleShieldScript bubbleShieldScript = null;
    [SerializeField] private PolarBearScript polarBearScript = null;

    [Header("Player camera")]
    // [SerializeField] private GameObject player = null;  // @Cleanup? This shouldn't be needed since this gets attached to the player object itself and not a child
    [SerializeField] private GameObject playerCamera = null;
    [SerializeField] private GameObject lookingAt = null;
    [SerializeField] private float yUpperBound = 4.0f;
    [SerializeField] private float yLowerBound = -1.0f;
    [SerializeField] private float rotationSpeedInverse = 1.0f;

    // -------------------------------------------------------------------------

    [HideInInspector] public Vector2 mouseInput;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public float isJumping = 0.0f;
    [HideInInspector] public float isDashing = 0.0f;
    [HideInInspector] public bool insideCastingZone = false;
    [HideInInspector] public bool useAbility = false;
    [HideInInspector] public bool useCombatAbility = false;
    [HideInInspector] public bool attack = false;

    private Rigidbody _rigidbody = null;
    private GameObject _playerMesh = null;

    // Jump stuff
    private bool _jumped = false;
    private bool _doubleJumped = false;
    private float _jumpCooldown = 0.0f;

    // Dash stuff
    private bool _dashed = false;
    private bool _airDashed = false;
    private float _dashCooldown = 0.0f;

    // Attack stuff
    private float _comboDuration = 0.0f;
    private float _animationDuration = 0.5f;
    public int comboCounter = 0;
    public float[] damageValues = new float[3];
    public float[] _animationDelay = new float[3];
    public AttackHitbox[] hitboxes;

    public bool _isGrounded = true;
    RaycastHit terrain;
    public float hopSpeed = 0.25f;
    public bool revive = false;
    public bool downed = false;
    SelectionWheelUI _wheelUI;
    public bool selectWheel = false;
    public bool _confirmWheel = false;
    public int _wheelSelection = 0;
    float _wheelCooldown = 2.0f;
    float _dashDuration = 0.0f;
    float _jumpAnimDuration = 0.0f;
    public bool outOfCombat = true;
    float _regenTicks = 0.0f;
    //NOTE: _mouseSpeed changes mouse sensitivity. Implement into options in the future
    float _mouseSpeed = 1.75f;
    PlayerBackend backend;

    private Animator _animator;

    public bool hitEnemy = false;
    public bool playAttackSound = false;
    private float turnSpeed;

    private float _abilityCD = 10.0f;
    private float _abilityDuration = 7.5f;
    private float _chargeDuration;
    private bool _charging = false;
    private float _chargeMultiplier = 3.5f;
    public ChargeHitbox abilityHitbox;


    // -------------------------------------------------------------------------

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        backend = this.GetComponentInParent<PlayerBackend>();
        backend.hp = backend.maxHP;
        setupPlayer();
    }

    void setupPlayer()
    {
        float percentage = backend.hp / backend.maxHP;

        switch (playerType)
        {

            case PlayerType.BISON:
                _setCombo(1.0f, 999999.0f, 50.0f, 0.7f, 1.0f, 1.10f);
                backend.maxHP = 1000;
                break;
            case PlayerType.POLAR_BEAR:
                _setCombo(1.0f, 999999.0f, 50.0f, 0.90f / 1.21f, 1.20f / 1.45f, 0.80f / 0.56f);
                backend.maxHP = 1000;

                break;
            case PlayerType.RATTLESNAKE:
                _setCombo(1.0f, 999999.0f, 20.0f, 0.35f, 0.75f, 1.10f);
                backend.maxHP = 1000;

                break;
            case PlayerType.TURTLE:
                _setCombo(1.0f, 999999.0f, 20.0f, 0.35f, 0.75f, 1.10f);
                backend.maxHP = 1000;

                break;
            default:
                _setCombo(1.0f, 999999.0f, 25.0f, 0.45f, 0.95f, 1.25f);
                break;
        }
        if(UseXinputScript.use)
            _playerMesh = GetComponentInParent<GameXinputHandler>().playerPrefabs[(int)playerType].prefab;
        else
            _playerMesh = GetComponentInParent<GameInputHandler>()._playerPrefabs[(int)playerType].prefab;
        backend.hp = backend.maxHP * percentage;
        hitboxes = _playerMesh.GetComponentsInChildren<AttackHitbox>(true);
        if (playerType == PlayerType.BISON)
            abilityHitbox = GetComponentInChildren<ChargeHitbox>(true);
        Debug.Log(hitboxes.Length);
    }

    void Update()
    {
        _comboDuration -= Time.deltaTime;
        _animationDuration -= Time.deltaTime;

        if (!downed)
        {
            if (insideCastingZone)
                _UseAbility();

            if (attack)
                _Attack();
            if (revive)
                _Revive();
        }
    }

    //Physics update (FixedUpdate); updates at set intervals
    private void FixedUpdate()
    {
        _jumpAnimDuration -= Time.fixedDeltaTime;
        _dashDuration -= Time.fixedDeltaTime;
        _dashCooldown -= Time.fixedDeltaTime;
        _wheelCooldown -= Time.fixedDeltaTime;
        _regenTicks -= Time.fixedDeltaTime;
        _abilityCD -= Time.fixedDeltaTime;

        if (!downed && _animationDuration <= 0.0f)
        {
            //Jump Movement
            if (isJumping == 1.0f)
                _Jump();

            //Dash Movement
            if (isDashing == 1.0f)
                _Dash();

            //Regen
            if (this.GetComponentInParent<PlayerBackend>().hp < this.GetComponentInParent<PlayerBackend>().maxHP && outOfCombat && _regenTicks < 0.0f)
            {
                _Regen();
                _regenTicks = 2.0f;
            }

            //Combat Ability
            if (useCombatAbility)
                _useCombatAbility();
        }
    }

    //Called after physics (FixedUpdate); used to prevent sliding on slopes due to high gravity
    private void LateUpdate()
    {
        if (_wheelUI == null)
        {
            _wheelUI = GameObject.Find("SelectionWheel").GetComponent<SelectionWheelUI>();
            _wheelUI.hideWheelUI();
        }
        if (selectWheel && _wheelCooldown <= 0.0f)
            _SelectionWheel();
        else
        {
            _MouseInput();
            _wheelUI.hideWheelUI();
        }

        if (!selectWheel && _confirmWheel)
            _ConfirmWheel();

        _Move();
    }


    // -------------------------------------------------------------------------

    private void _setCombo(float x, float y, float z, float u, float v, float w)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        damageValues[0] = x;
        damageValues[1] = y;
        damageValues[2] = z;
        _animationDelay[0] = u;
        _animationDelay[1] = v;
        _animationDelay[2] = w;
    }

    void _MouseInput()
    {
        if (rotationSpeedInverse < 1.0f)
            rotationSpeedInverse = 1.0f;

        //rotate camera on x z plane
        playerCamera.transform.RotateAround(transform.position, Vector3.up, mouseInput.x / rotationSpeedInverse * _mouseSpeed);
        //increase/decrease height of camera target
        lookingAt.transform.position = lookingAt.transform.position + new Vector3(0.0f, mouseInput.y / (rotationSpeedInverse * 10.0f), 0.0f);


        //clamp y position
        if (lookingAt.transform.localPosition.y > yUpperBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yUpperBound, lookingAt.transform.localPosition.z);
        else if (lookingAt.transform.localPosition.y < yLowerBound)
            lookingAt.transform.localPosition = new Vector3(lookingAt.transform.localPosition.x, yLowerBound, lookingAt.transform.localPosition.z);

        //lock on to target
        playerCamera.transform.LookAt(lookingAt.transform);
    }

    void _SelectionWheel()
    {
        _confirmWheel = true;
        float absX = Mathf.Abs(mouseInput.x);
        float absY = Mathf.Abs(mouseInput.y);
        //NOTE: Sets _wheelSelection to the appropriate animal and highlights their part of the selection wheel
        if (absY > 0.25f || absX > 0.25f)
        {
            if (absX >= absY)
            {
                if (mouseInput.x > 0.0f)
                {
                    //set 1, rattlesnake
                    _wheelSelection = 1;
                }
                else
                {
                    //set 3, bison
                    _wheelSelection = 3;
                }
            }
            else
            {
                if (mouseInput.y > 0.0f)
                {
                    //set 0, turtle
                    _wheelSelection = 0;
                }
                else
                {
                    //set 2, polar bear
                    _wheelSelection = 2;
                }
            }
            _wheelUI.normalizeWheelUI();
            _wheelUI.highlightWheelUI(_wheelSelection);
        }
        else
        {
            //Turn off highlights
            _confirmWheel = false;
            _wheelUI.normalizeWheelUI();
        }
    }

    void _ConfirmWheel()
    {
        _wheelUI.hideWheelUI();
        _confirmWheel = false;
        if (_wheelSelection != (int)playerType)
        {
            _wheelCooldown = 2.0f;
            if (UseXinputScript.use)
            {
                GetComponentInParent<GameXinputHandler>().swapPlayer(_wheelSelection);
                _animator = GetComponentInParent<GameXinputHandler>()._animator;
            }
            else
            {
                GetComponentInParent<GameInputHandler>().swapPlayer(_wheelSelection);
                _animator = GetComponentInParent<GameInputHandler>()._animator;
            }


            setupPlayer();
        }
    }

    void _Move()
    {
        if (_animator)
            _animator.SetBool("walking", true);

        if (_dashDuration < 0.0f && _animationDuration < 0.0f && !_charging)
        {
            //NOTE: Camera position affects the rotation of the player's movement, which is stored in the first value of Vector3 vel (Current: 135.0f)
            Vector3 vel = playerCamera.transform.right*moveInput.x + playerCamera.transform.forward * moveInput.y;
            vel *= moveSpeed;
            if (vel.magnitude >= 0.1f)
            {
                _playerMesh.transform.rotation = Quaternion.Euler(0.0f,Mathf.SmoothDampAngle(_playerMesh.transform.eulerAngles.y,playerCamera.transform.eulerAngles.y,ref turnSpeed,0.25f),0.0f);
            }
            float y = _rigidbody.velocity.y;
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
            if (downed)
                vel = Vector3.zero;
            _rigidbody.velocity = new Vector3(vel.x, y, vel.z);
        }
    }

    void _Jump()
    {
        //NOTE: Resets the button so that the player doesn't accidentally double jump
        isJumping = 0.0f;
        //NOTE: Jumping adds a slight boost to the x/z direction you move in to simulate push back against the ground
        //NOTE: Gravity is set to -24.525f. To change it: Edit -> Project Settings -> Physics -> y = newGravityValue
        float jump = Mathf.Sqrt(jumpSpeed * -2.0f * -24.525f);

        if (_jumpAnimDuration <= 0.0f && _isGrounded && !_jumped)
        {
            Vector3 vel = _rigidbody.velocity;
            _rigidbody.AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _jumped = true;
            _jumpAnimDuration = 0.3f;
        }
        else if (_jumpAnimDuration <= 0.0f && !_doubleJumped)
        {
            Vector3 vel = _rigidbody.velocity;
            //NOTE: Adjusts double jump to give full jump height rather than being affected by gravity. Remove if necessary
            _rigidbody.velocity = new Vector3(vel.x, 0.0f, vel.z);
            _rigidbody.AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _doubleJumped = true;
            _jumpAnimDuration = 0.3f;
        }
    }

    void _Dash()
    {
        //NOTE: Resets the button so that the player doesn't accidentally dash + air dash while holding the button
        isDashing = 0.0f;

        //NOTE: _dashCooldown stops dash chaining that would lead to acceleration
        if (_dashCooldown <= 0.0f && _dashDuration <= 0.0f && !_dashed && _isGrounded)
        {
            Vector3 vel = _rigidbody.velocity;
            _rigidbody.AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
            _dashed = true;
            _dashDuration = 0.35f;
            _dashCooldown = 0.5f;
        }
        else if (_dashCooldown <= 0.0f && _dashDuration <= 0.0f && !_airDashed)
        {
            Vector3 vel = _rigidbody.velocity;
            _rigidbody.AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
            _airDashed = true;
            _dashDuration = 0.35f;
            _dashCooldown = 0.5f;
        }
    }

    void _UseAbility()
    {
        // @Todo: activate some prompt

        if (!useAbility)
        {
            if (_animator)
                _animator.SetBool("ability", false);
            return;
        }
        if (_animator)
            _animator.SetBool("ability", true);

        //add more...wait i dont think we need to add more than one lmaooooooooooooooooooooooooo
        switch (playerType)
        {
            case PlayerType.TURTLE: bubbleShieldScript.AttemptToCast(); break;
            case PlayerType.POLAR_BEAR: polarBearScript.Transition(); break;
            case PlayerType.BISON: ramThrough.hasRammed = true; break;
        }
        useAbility = false;
    }

    void _useCombatAbility()
    {
        if (_abilityCD > 0.0f)
            return;
    Debug.Log(playerType);
        switch (playerType)
        {
            case PlayerType.TURTLE: StartCoroutine(Buff()); break;
            case PlayerType.POLAR_BEAR: StartCoroutine(Roar()); break;
            case PlayerType.RATTLESNAKE: StartCoroutine(Fear()); break;
            case PlayerType.BISON: StartCoroutine(Charge()); break;
        }
    }
    IEnumerator Buff()
    {
        Debug.Log("Start Buff");
        _abilityCD = 10.0f;
        var damage = damageValues;
        for (int i=0;i<damageValues.Length;i++)
            damageValues[i] *= 1.1f;
        var speed = moveSpeed;
        moveSpeed *= 1.1f;
        Debug.Log(speed);
        Debug.Log(moveSpeed);
        // Implement debuff cleansing once debuffs are in
        GetComponent<PlayerBackend>().turtleBuff = true;
        // Waits for _abilityDuration
        yield return new WaitForSeconds(_abilityDuration);

        // Resets values
        damageValues = damage;
        moveSpeed = speed;
        Debug.Log("End Buff");
        Debug.Log(speed);
        Debug.Log(moveSpeed);

    }
    IEnumerator Roar()
    {
        _abilityCD = 10.0f;
        yield return new WaitForSeconds(_abilityDuration);
    }
    IEnumerator Fear()
    {
        _abilityCD = 10.0f;
        yield return new WaitForSeconds(_abilityDuration);
    }
    IEnumerator Charge()
    {
        Debug.Log("Start Charge");
        _abilityCD = 10.0f;
        _chargeDuration = _abilityDuration;
        _dashCooldown = _abilityDuration;
        _jumpCooldown = _abilityDuration;
        _charging = true;
        var turn = turnSpeed;
        turnSpeed *=_chargeMultiplier;
        GetComponent<PlayerBackend>().invuln = true;
        abilityHitbox.gameObject.SetActive(true);
        while (_chargeDuration > 0.0f)
        {
            _chargeDuration -= Time.deltaTime;
            _rigidbody.velocity = new Vector3(playerCamera.transform.forward.x*moveSpeed*_chargeMultiplier, -1.0f, playerCamera.transform.forward.z*moveSpeed*_chargeMultiplier);
            _playerMesh.transform.rotation = Quaternion.Euler(0.0f,Mathf.SmoothDampAngle(_playerMesh.transform.eulerAngles.y,playerCamera.transform.eulerAngles.y,ref turnSpeed,0.05f),0.0f);
            yield return null;
        }
        _charging = false;
        GetComponent<PlayerBackend>().invuln = false;
        turnSpeed = turn;
        abilityHitbox.gameObject.SetActive(false);
        Debug.Log("End Charge");
    }

    void _Attack()
    {
        if (_animationDuration >= 0.0f)
        {
            return;
        }
        if (_comboDuration < 0.0f) comboCounter = 0;
        _animationDuration = _animationDelay[comboCounter];

        if (_animator)
        {

            switch (comboCounter)
            {
                case 0:
                    _animator.SetTrigger("attack1");
                    break;
                case 1:
                    _animator.SetTrigger("attack2");
                    break;
                case 2:
                    _animator.SetTrigger("attack3");
                    break;
                default:
                    _animator.SetTrigger("attack1");
                    break;
            }

        }
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(_playerMesh.transform.forward*attackDistance, ForceMode.Impulse);
        /*
        RaycastHit enemy;
        if(Physics.Raycast(transform.position, _playerMesh.transform.forward, out enemy, 2.0f) && enemy.transform.tag == "Enemy") {
            EnemyData foe = enemy.collider.GetComponent<EnemyData>();
            foe.takeDamage(damageValues[comboCounter]);
            hitEnemy = true;
        }
*/
        comboCounter++;
        if (comboCounter > 2) comboCounter = 0;
        _comboDuration = 2.0f;

        attack = false;
        playAttackSound = true;
    }

    void _Revive()
    {
        RaycastHit player;
        if (Physics.Raycast(_playerMesh.transform.position, transform.forward, out player, 5.0f) && player.transform.tag == "Player")
        {
            PlayerController revivee = player.collider.gameObject.GetComponent<PlayerController>();
            if (backend.hp > 0.0f)
            {
                float _hpTransfer = backend.hp / 2.0f;
                backend.hp /= 2.0f;
                revivee.GetComponentInParent<PlayerBackend>().hp += _hpTransfer;
                revivee.downed = false;
            }
        }
        revive = false;
    }

    void _Regen()
    {
        this.GetComponentInParent<PlayerBackend>().hp += 10.0f;
    }
}
