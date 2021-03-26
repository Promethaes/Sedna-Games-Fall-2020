using System;
using System.Collections;
using Unity;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    [Header("Player variables")]

    [SerializeField] private float _originalSpeed = 12.0f;
    [SerializeField] private float moveSpeed = 12.0f;
    [SerializeField] private float jumpSpeed = 3.0f;
    [SerializeField] private float dashSpeed = 2.5f;
    [SerializeField] private float attackDistance = 8.0f;

    [Header("Player abilities")]
    // Default mesh is turtle, if a turtle has bison abilities, either this or the player mesh in GameInputHandler did not get set properly
    public PlayerType playerType = PlayerType.BISON;

    public AbilityScript abilityScript = null;

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
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool insideCastingZone = false;
    [HideInInspector] public bool useAbility = false;
    [HideInInspector] public bool useCombatAbility = false;
    [HideInInspector] public bool attack = false;
    [HideInInspector] public bool toggle = false;

    private Rigidbody _rigidbody = null;
    public GameObject _playerMesh = null;

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
    public float[] originalDamageValues = new float[3];
    public float[] _animationDelay = new float[3];
    public AttackHitbox[] hitboxes;

    RaycastHit terrain;
    SelectionWheelUI _wheelUI;
    public bool _isGrounded = true;
    public float hopSpeed = 0.25f;
    public bool revive = false;
    public bool downed = false;
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
    private float _chargeDuration = 3.0f;
    private bool _charging = false;
    private float _chargeMultiplier = 3.5f;
    public ChargeHitbox abilityHitbox;
    public int killCount;
    public bool roarBuff = false;
    public bool venomBuff = false;

    // Debuffs
    bool _slowDebuff = false;
    bool _poisonDebuff = false;
    float _slowDuration;
    float _poisonDuration;

    //
    public bool inCutscene = false;
    public GameObject questUI;

    // VFX
    public ParticleSystem dashVFX;

    public float knockbackScalar = 20.0f;

    // -------------------------------------------------------------------------


    //Network variables
    public bool sendPlayerChanged = false;
    public bool remotePlayer = false;
    public bool sendAttack = false;
    public bool sendJump = false;
    public bool sendMovement = false;
    public string userName = "";
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        backend = this.GetComponentInParent<PlayerBackend>();
        backend.hp = backend.maxHP;
        setupPlayer();
        StartCoroutine(SetupWheelUI());
        StartCoroutine(SetupQuestUI());
    }

    IEnumerator SetupWheelUI()
    {
        while (_wheelUI == null)
            yield return new WaitForSeconds(5.0f);
    }

    IEnumerator SetupQuestUI()
    {
        while (questUI == null)
        {
            questUI = GameObject.Find("QuestsUI");
            yield return new WaitForSeconds(2.5f);
        }
    }

    void setupPlayer()
    {
        float percentage = backend.hp / backend.maxHP;
        abilityScript = GameObject.Find("AbilityManager").GetComponent<AbilityScript>();
        switch (playerType)
        {
            case PlayerType.BISON:
                _setCombo(10.0f, 25.0f, 35.0f, 0.7f, 1.0f, 1.10f);
                backend.maxHP = 250;
                //temp, please remove in refactor
                knockbackScalar = 50.0f;
                break;
            case PlayerType.POLAR_BEAR:
                _setCombo(10.0f, 35.0f, 60.0f, 0.90f / 1.21f, 1.20f / 1.45f, 0.80f / 0.56f);
                backend.maxHP = 150;
                knockbackScalar = 25.0f;
                break;
            case PlayerType.RATTLESNAKE:
                _setCombo(25.0f, 50.0f, 150.0f, 0.35f, 0.75f, 1.10f);
                backend.maxHP = 50;
                knockbackScalar = 10.0f;
                break;
            case PlayerType.TURTLE:
                _setCombo(10.0f, 25.0f, 50.0f, 0.35f, 0.75f, 1.10f);
                backend.maxHP = 100;
                knockbackScalar = 10.0f;
                break;
            default:
                _setCombo(1.0f, 999999.0f, 25.0f, 0.45f, 0.95f, 1.25f);
                break;
        }
        _playerMesh = GetComponentInParent<GameInputHandler>()._playerPrefabs[(int)playerType].prefab;

        backend.hp = backend.maxHP * percentage;
        hitboxes = _playerMesh.GetComponentsInChildren<AttackHitbox>(true);
        if (playerType == PlayerType.BISON)
            abilityHitbox = GetComponentInChildren<ChargeHitbox>(true);
        damageValues = originalDamageValues;
    }
    Vector3 nLastPos = new Vector3();
    void SendMovemnt()
    {
        if ((gameObject.transform.position - nLastPos).magnitude != 0.0f)
            sendMovement = true;
        nLastPos = gameObject.transform.position;
    }

    void Update()
    {
        _comboDuration -= Time.deltaTime;
        _animationDuration -= Time.deltaTime;

        if (downed || inCutscene)
        {
            return;
        }

        if (_animator)
        {
            _animator.SetBool("attacking", false);
            _animator.SetBool("ability", false);
        }

        if (insideCastingZone && useAbility)
            _UseAbility();

        if (attack) _Attack();
        if (revive) _Revive();

        if (!remotePlayer)
            SendMovemnt();
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

        if (!downed && _animationDuration <= 0.0f && !inCutscene)
        {
            //Jump Movement
            if (isJumping)
                _Jump();

            //Dash Movement
            if (isDashing)
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

            if (toggle)
                _Toggle();

            if (remotePlayer)
                _rigidbody.velocity = Vector3.zero;
        }
    }

    //Called after physics (FixedUpdate); used to prevent sliding on slopes due to high gravity
    private void LateUpdate()
    {
        if (!downed && !inCutscene)
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
        }
        _Move();
    }


    // -------------------------------------------------------------------------

    private void _setCombo(float x, float y, float z, float u, float v, float w)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        originalDamageValues[0] = x;
        originalDamageValues[1] = y;
        originalDamageValues[2] = z;
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
        if (GameObject.FindGameObjectWithTag("camOBJ").GetComponent<CameraObject>())
        {
            CameraObject ourCamOBJ = GameObject.FindGameObjectWithTag("camOBJ").GetComponent<CameraObject>();

            ourCamOBJ.MoveUpDown(mouseInput.y);
        }
        else
            Debug.LogError("CameraObject not Tagged!");


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
                if (mouseInput.x > 0.0f)
                    //set 1, rattlesnake
                    _wheelSelection = 1;
                else
                    //set 3, bison
                    _wheelSelection = 3;
            else
                if (mouseInput.y > 0.0f)
                //set 0, turtle
                _wheelSelection = 0;
            else
                //set 2, polar bear
                _wheelSelection = 2;
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

    public void ChangeCharFromNetwork(int selec)
    {
        GetComponentInParent<GameInputHandler>().swapPlayer(selec);
        _animator = GetComponentInParent<GameInputHandler>()._animator;

        setupPlayer();


    }

    void _ConfirmWheel()
    {
        _wheelUI.hideWheelUI();
        _confirmWheel = false;
        if (_wheelSelection != (int)playerType)
        {
            _wheelCooldown = 2.0f;

            GetComponentInParent<GameInputHandler>().swapPlayer(_wheelSelection);
            _animator = GetComponentInParent<GameInputHandler>()._animator;

            sendPlayerChanged = true;
            setupPlayer();

            var skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var smr in skinnedMeshRenderers)
            {
                if (smr.gameObject.transform.parent.parent.gameObject.activeSelf)
                {
                    CameraObject.ChangeSkinnedMesh(smr);
                    GameObject temp = GameObject.FindGameObjectWithTag("camOBJ");
                    temp.GetComponentInChildren<CameraObject>().changeParent();
                }
            }

        }
    }

    Vector3 _lastPos = Vector3.zero;
    void _Move()
    {
        //i hate duplicate code
        if (remotePlayer)
        {
            var pos = gameObject.transform.position;
            pos.y = 0.0f;
            if (_animator && !_animator.GetBool("attacking") && !_animator.GetBool("jumping"))
                _animator.SetBool("walking", Mathf.Abs(pos.magnitude - _lastPos.magnitude) >= 0.1f);

            _lastPos = pos;
            _isGrounded = Physics.Raycast(transform.position, -transform.up, out terrain, 0.6f);
            if (_isGrounded && terrain.transform.tag == "Terrain")
            {
                _dashed = false;
                _airDashed = false;
                if (_jumpAnimDuration <= 0.0f)
                {
                    _jumped = false;
                    _doubleJumped = false;
                    if (_animator) _animator.SetBool("jumping", false);
                }
            }
            return;
        }

        if (_dashDuration < 0.0f && !_charging)
        {

            //NOTE: Camera position affects the rotation of the player's movement, which is stored in the first value of Vector3 vel (Current: 135.0f)
            Vector3 vel = playerCamera.transform.right * moveInput.x + playerCamera.transform.forward * moveInput.y;
            vel *= moveSpeed;
            //if (vel.magnitude >= 0.1f)
            {
                float dashX = moveInput.x * -1.0f * 90.0f;
                _playerMesh.transform.rotation = Quaternion.Euler(0.0f, Mathf.SmoothDampAngle(_playerMesh.transform.eulerAngles.y, playerCamera.transform.eulerAngles.y, ref turnSpeed, 0.25f), 0.0f);

            }
            float y = _rigidbody.velocity.y;
            //NOTE: Checks for _isGrounded to reduce the effects of gravity such that the player doesn't slide off slopes
            //TODO: Adjust raycast for actual models' radii
            //NOTE: Raycasts downwards for terrain collision, checking at a distance of 0.6f (0.5f radius, 0.1f actual check)
            _isGrounded = Physics.Raycast(transform.position, -transform.up, out terrain, 0.6f);
            if (_isGrounded && terrain.transform.tag == "Terrain" && y < 1.0f)
            {
                y = -1.0f;

                //NOTE: Updates the state of dashes and jumps
                _dashed = false;
                _airDashed = false;
                if (_jumpAnimDuration <= 0.0f)
                {
                    _jumped = false;
                    _doubleJumped = false;
                    if (_animator) _animator.SetBool("jumping", false);
                }
            }
            if (downed || inCutscene)
                vel = Vector3.zero;

            _rigidbody.velocity = new Vector3(vel.x, y, vel.z);
            dashVFX.transform.forward = -_rigidbody.velocity.normalized;
            if (_animator) _animator.SetBool("walking", vel.magnitude >= 0.1f);
        }
    }

    void _Jump()
    {
        //NOTE: Resets the button so that the player doesn't accidentally double jump
        isJumping = false;

        if (remotePlayer)
        {
            if (_animator)
            {
                _animator.SetBool("walking", false);
                _animator.SetBool("jumping", true);
            }
            return;
        }

        //NOTE: Jumping adds a slight boost to the x/z direction you move in to simulate push back against the ground
        //NOTE: Gravity is set to -24.525f. To change it: Edit -> Project Settings -> Physics -> y = newGravityValue
        float jump = Mathf.Sqrt(jumpSpeed * -2.0f * -24.525f);


        if (_jumpAnimDuration <= 0.0f && _isGrounded && !_jumped)
        {
            sendJump = true;
            Vector3 vel = _rigidbody.velocity;
            _rigidbody.AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _jumped = true;
            _jumpAnimDuration = 0.3f;
            if (_animator) _animator.SetBool("jumping", true);
        }
        else if (_jumpAnimDuration <= 0.0f && !_doubleJumped)
        {
            Vector3 vel = _rigidbody.velocity;
            //NOTE: Adjusts double jump to give full jump height rather than being affected by gravity. Remove if necessary
            _rigidbody.velocity = new Vector3(vel.x, 0.0f, vel.z);
            _rigidbody.AddForce(new Vector3(vel.x * hopSpeed, jump, vel.z * hopSpeed), ForceMode.Impulse);
            _doubleJumped = true;
            _jumpAnimDuration = 0.3f;
            if (_animator) _animator.SetTrigger("double_jump");
        }
    }

    void _Dash()
    {
        //NOTE: Resets the button so that the player doesn't accidentally dash + air dash while holding the button
        isDashing = false;

        //NOTE: _dashCooldown stops dash chaining that would lead to acceleration
        if (_dashCooldown <= 0.0f && _dashDuration <= 0.0f)
        {
            if (_animator) _animator.SetTrigger("dash");

            if (!_dashed && _isGrounded)
            {
                dashVFX.gameObject.SetActive(true);
                dashVFX.Play();
                Vector3 vel = _rigidbody.velocity;
                _rigidbody.AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
                _dashed = true;
                _dashDuration = 0.35f;
                _dashCooldown = 0.5f;
            }
            else if (!_airDashed)
            {
                dashVFX.gameObject.SetActive(true);
                dashVFX.Play();
                Vector3 vel = _rigidbody.velocity;
                _rigidbody.AddForce(new Vector3(vel.x * dashSpeed, 0.0f, vel.z * dashSpeed), ForceMode.Impulse);
                _airDashed = true;
                _dashDuration = 0.35f;
                _dashCooldown = 0.5f;
            }
        }
    }

    public bool sendUsedAbility = false;
    void _UseAbility()
    {
        if (_animator) _animator.SetBool("ability", true);
        StartCoroutine(abilityScript.enterQTE(this));
        useAbility = false;
        sendUsedAbility = true;
    }
    public bool sendUsedCombatAbility = false;
    void _useCombatAbility()
    {
        if (_abilityCD > 0.0f) return;
        sendUsedCombatAbility = true;
        switch (playerType)
        {
            case PlayerType.TURTLE: StartCoroutine(Buff()); break;
            case PlayerType.POLAR_BEAR: StartCoroutine(Roar()); break;
            case PlayerType.RATTLESNAKE: StartCoroutine(Venom()); break;
            case PlayerType.BISON: StartCoroutine(Charge()); break;
        }
    }

    public void slowed()
    {
        if (!_slowDebuff && _slowDuration <= 0.0f)
            StartCoroutine(SlowDebuff());
        _slowDebuff = true;
        _slowDuration = 7.5f;
    }

    public void poisoned()
    {
        if (!_poisonDebuff && _poisonDuration <= 0.0f)
            StartCoroutine(PoisonDebuff());
        _poisonDebuff = true;
        _poisonDuration = 5.0f;
    }
    IEnumerator SlowDebuff()
    {
        Coroutine _speedFormula = StartCoroutine(SpeedFormula());
        while (_slowDuration > 0.0f)
        {
            yield return new WaitForSeconds(2.5f);
            _slowDuration -= 2.5f;
        }

        _slowDebuff = false;
        yield return null;
    }
    IEnumerator PoisonDebuff()
    {
        while (_poisonDuration > 0.0f)
        {
            GetComponent<PlayerBackend>().takeDamage(3.0f);
            yield return new WaitForSeconds(1.0f);
            _poisonDuration -= 1.0f;
        }
        _poisonDebuff = false;
        yield return null;
    }

    IEnumerator Buff()
    {
        Debug.Log("Start Buff");
        GetComponent<PlayerBackend>().turtleBuff = true;
        Coroutine _damageFormula = StartCoroutine(DamageFormula());
        Coroutine _speedFormula = StartCoroutine(SpeedFormula());

        //TODO: Implement debuff cleansing once debuffs are in
        _poisonDuration = 0.0f;
        _slowDuration = 0.0f;
        _slowDebuff = false;
        _poisonDebuff = false;
        // Waits for _abilityDuration
        yield return new WaitForSeconds(_abilityDuration);

        // Resets values
        GetComponent<PlayerBackend>().turtleBuff = false;

        _abilityCD = 10.0f;
        Debug.Log("End Buff");

    }
    IEnumerator DamageFormula()
    {
        var _players = GetComponentInParent<GamePlayerManager>().players;
        while (GetComponent<PlayerBackend>().turtleBuff || roarBuff)
        {
            for (int i = 0; i < _players.Count; i++)
                for (int n = 0; n < damageValues.Length; n++)
                    _players[i].GetComponent<PlayerController>().damageValues[n] = _players[i].GetComponent<PlayerController>().originalDamageValues[n]
                    + _players[i].GetComponent<PlayerController>().originalDamageValues[n] * _players[i].GetComponent<PlayerBackend>().turtleBuff.GetHashCode() * 0.1f
                    + _players[i].GetComponent<PlayerController>().originalDamageValues[n] * roarBuff.GetHashCode() * Mathf.Min(killCount, 3) * .05f;

            for (int n = 0; n < damageValues.Length; n++)
                damageValues[n] += 25.0f * (playerType == PlayerType.POLAR_BEAR).GetHashCode();
            yield return new WaitForSeconds(1);
        }
        for (int n = 0; n < damageValues.Length; n++)
            damageValues[n] = originalDamageValues[n]
            + originalDamageValues[n] * GetComponent<PlayerBackend>().turtleBuff.GetHashCode() * 0.1f
            + originalDamageValues[n] * roarBuff.GetHashCode() * Mathf.Min(killCount, 3) * .05f;
        yield return null;
    }
    IEnumerator SpeedFormula()
    {
        while (GetComponent<PlayerBackend>().turtleBuff || _slowDebuff)
        {
            moveSpeed = _originalSpeed + _originalSpeed * GetComponent<PlayerBackend>().turtleBuff.GetHashCode() * 0.1f - _originalSpeed * _slowDebuff.GetHashCode() * 0.1f;
            yield return new WaitForSeconds(1);
        }
        moveSpeed = _originalSpeed + _originalSpeed * GetComponent<PlayerBackend>().turtleBuff.GetHashCode() * 0.1f - _originalSpeed * _slowDebuff.GetHashCode() * 0.1f;
        yield return null;
    }
    IEnumerator Roar()
    {
        killCount = 0;
        roarBuff = true;
        Coroutine _damageFormula = StartCoroutine(DamageFormula());

        yield return new WaitForSeconds(_abilityDuration);

        // Reset values
        killCount = 0;
        roarBuff = false;

        _abilityCD = 10.0f;
        yield return null;
    }
    IEnumerator Venom()
    {
        venomBuff = true;
        yield return new WaitForSeconds(_abilityDuration);
        venomBuff = false;
        _abilityCD = 10.0f;
        yield return null;
    }
    IEnumerator Charge()
    {
        Debug.Log("Start Charge");
        _chargeDuration = 3.0f;
        _dashCooldown = _chargeDuration;
        _jumpCooldown = _chargeDuration;
        _charging = true;
        var turn = turnSpeed;
        turnSpeed *= _chargeMultiplier;
        GetComponent<PlayerBackend>().invuln = true;
        abilityHitbox.gameObject.SetActive(true);
        while (_chargeDuration > 0.0f)
        {
            _chargeDuration -= Time.deltaTime;
            _rigidbody.velocity = new Vector3(playerCamera.transform.forward.x * moveSpeed * _chargeMultiplier, -1.0f, playerCamera.transform.forward.z * moveSpeed * _chargeMultiplier);
            _playerMesh.transform.rotation = Quaternion.Euler(0.0f, Mathf.SmoothDampAngle(_playerMesh.transform.eulerAngles.y, playerCamera.transform.eulerAngles.y, ref turnSpeed, 0.05f), 0.0f);
            yield return null;
        }
        _charging = false;
        GetComponent<PlayerBackend>().invuln = false;
        turnSpeed = turn;
        abilityHitbox.gameObject.SetActive(false);
        _abilityCD = 10.0f;
        Debug.Log("End Charge");
    }

    void _Attack()
    {
        if (_animationDuration >= 0.0f) return;

        //TODO
        {
            GameObject temp = GameObject.FindGameObjectWithTag("virtualCam");
            temp.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = GameObject.FindGameObjectWithTag("TARGET").transform;
        }
       

        sendAttack = true;

        if (_comboDuration < 0.0f) comboCounter = 0;
        _animationDuration = _animationDelay[comboCounter];

        if (_animator)
        {
            _animator.SetBool("attacking", true);
            switch (comboCounter)
            {
                case 0: _animator.SetTrigger("attack1"); break;
                case 1: _animator.SetTrigger("attack2"); break;
                case 2: _animator.SetTrigger("attack3"); break;
                default: _animator.SetTrigger("attack1"); break;
            }
        }

        if (!remotePlayer)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(_playerMesh.transform.forward * attackDistance, ForceMode.Impulse);
        }


        RaycastHit enemy;
        if (Physics.Raycast(transform.position, _playerMesh.transform.forward, out enemy, 5.0f) && enemy.transform.tag == "Enemy")
        {
            EnemyData foe = enemy.collider.GetComponent<EnemyData>();
            foe.takeDamage(damageValues[comboCounter]);
            hitEnemy = true;
        }

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

    void _Toggle()
    {
        toggle = false;
        questUI.SetActive(!questUI.activeSelf);
    }

    void _Regen()
    {
        this.GetComponentInParent<PlayerBackend>().hp += 10.0f;
    }
}
