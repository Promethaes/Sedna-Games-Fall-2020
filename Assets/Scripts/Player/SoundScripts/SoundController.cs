using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class SoundController : MonoBehaviour
{

    public StudioEventEmitter attackSound;
    public StudioEventEmitter impactSound;
    public StudioEventEmitter jumpSound;
    public StudioEventEmitter landSound;
    public StudioEventEmitter movementSound;
    public StudioEventEmitter charSwapSound;
    public StudioEventEmitter selectSound;
    PlayerController _playerController;

    //KO, pain sounds are used in playerbackend
    //ability sound used in player controller

    public StudioEventEmitter[] polarBearSounds;
    public StudioEventEmitter[] bisonSounds;
    public StudioEventEmitter[] snakeSounds;
    public StudioEventEmitter[] turtleSounds;

    enum Index : int
    {
        Attack,
        CharSwapSpawn,
        KO,
        Pain,
        Ability
    }

    private List<StudioEventEmitter[]> characterSounds = new List<StudioEventEmitter[]>();

    Vector3 _lastPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        _timer = timer;
        _playerController = GetComponent<PlayerController>();
        characterSounds.Add(turtleSounds);
        characterSounds.Add(snakeSounds);
        characterSounds.Add(polarBearSounds);
        characterSounds.Add(bisonSounds);

        characterSounds[(int)_playerController.playerType][(int)Index.CharSwapSpawn].Play();
    }

    public float timer = 0.5f;
    float _timer = 0.5f;
    bool landed = false;
    int _lastWheel = -1;

    public void PlayPainSound()
    {
        characterSounds[(int)_playerController.playerType][(int)Index.Pain].Play();
    }

    public void PlayKOSound()
    {
        characterSounds[(int)_playerController.playerType][(int)Index.KO].Play();
    }

    public void PlayAbilitySound()
    {
        characterSounds[(int)_playerController.playerType][(int)Index.Ability].Play();
    }

    IEnumerator PlayCharSpawnSound()
    {
        yield return new WaitForSeconds(0.25f);
        characterSounds[(int)_playerController.playerType][(int)Index.CharSwapSpawn].Play();
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        Vector2 v2New = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector2 v2Old = new Vector2(_lastPosition.x, _lastPosition.z);

        if ((v2New - v2Old).magnitude >= 0.01f && _timer <= 0.0f && _playerController._isGrounded)
        {
            _timer = timer;
            movementSound.Play();
        }

        if (!landed && _playerController._isGrounded)
        {
            landSound.Play();
            landed = true;
        }

        if (!_playerController._isGrounded)
            landed = false;

        _lastPosition = gameObject.transform.position;

        if (_playerController.isJumping)
            jumpSound.Play();

        if (_playerController.selectWheel && _playerController._wheelSelection != _lastWheel)
        {
            charSwapSound.Play();
            _lastWheel = _playerController._wheelSelection;
        }

        if (!_playerController.selectWheel && _playerController._confirmWheel)
        {
            selectSound.Play();
            characterSounds[(int)_playerController.playerType][(int)Index.CharSwapSpawn].Stop();
            StartCoroutine("PlayCharSpawnSound");
        }

        if (_playerController.playAttackSound)
        {
            attackSound.Play();
            _playerController.playAttackSound = false;
            characterSounds[(int)_playerController.playerType][(int)Index.Attack].Play();
        }
        if (_playerController.hitEnemy)
        {
            _playerController.hitEnemy = false;
            impactSound.Play();
        }

    }

}
