using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashRed : MonoBehaviour
{
    [SerializeField]
    private Material _red = null;
    [SerializeField]
    private Material _original = null;
    [SerializeField]
    private float _flashTime = 0.1f;
    private SkinnedMeshRenderer _renderer;

    private float _changeTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<SkinnedMeshRenderer>())
            _renderer = gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_renderer)
            if (_changeTime > 0.0f)
            {
                _changeTime -= Time.deltaTime;
                if (_changeTime <= 0.0f)
                    _renderer.material = _original;
            }
    }
    void flash()
    {
        if (_renderer)
        {
            _renderer.material = _red;
            _changeTime = _flashTime;
        }
    }
}
