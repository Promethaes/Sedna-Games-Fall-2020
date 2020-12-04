using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Footsteps : MonoBehaviour
{
    public StudioEventEmitter emitter;
    Vector3 _lastPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        _timer = timer;
    }

    public float timer = 0.5f;
    float _timer = 0.5f;
    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        Vector2 v2New = new Vector2(gameObject.transform.position.x,gameObject.transform.position.z);
        Vector2 v2Old = new Vector2(_lastPosition.x,_lastPosition.z);

        if ((v2New - v2Old).magnitude != 0.0f)
        {

            if (_timer <= 0.0f)
            {
                _timer = timer;
                emitter.Play();

            }
        }
        _lastPosition = gameObject.transform.position;
    }
}
