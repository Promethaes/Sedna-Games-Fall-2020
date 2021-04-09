using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    float _duration;
    public void destroyVFX(float x)
    {
        _duration = x;
        StartCoroutine(_destroyVFX());
    }
    IEnumerator _destroyVFX()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }
}
