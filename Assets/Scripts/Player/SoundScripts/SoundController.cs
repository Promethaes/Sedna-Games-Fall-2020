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



    Vector3 _lastPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        _timer = timer;
        _playerController = GetComponent<PlayerController>();
    }

    public float timer = 0.5f;
    float _timer = 0.5f;
    bool landed = false;
    // Update is called once per frame
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
        if (_playerController.isJumping == 1.0f)
        {
            jumpSound.Play();
        }

        if (!_playerController.selectWheel && _playerController._confirmWheel)
            charSwapSound.Play();
            
    }

}
