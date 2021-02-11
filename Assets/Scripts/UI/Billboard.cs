using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    float _fadeTime = 0.0f;
    Image[] _images;

    private void Start() {
        _images = GetComponentsInChildren<Image>();
    }

    public void healthChanged()
    {
        _fadeTime = 3.5f;
            for (int i=0;i<_images.Length;i++)
            {
                Color _color = _images[i].color;
                _color.a = 1.0f;
                _images[i].color = _color;
            }
    }

    // Update is called once per frame
    void Update()
    {
        _fadeTime -= Time.deltaTime;
        if (_fadeTime <= 1.0f)
            for (int i=0;i<_images.Length;i++)
            {
                Color _color = _images[i].color;
                _color.a = _fadeTime;
                _images[i].color = _color;
            }
        //if (_fadeTime <= 0.0f)
        //TODO: When activating a cutscene, the game can't tell what the main camera is, causes game to break
        //transform.LookAt(Camera.main.transform);
    }
}
