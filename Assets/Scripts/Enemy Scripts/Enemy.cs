using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    protected float _health;
    protected float[] _range = new float[3];
    protected float _animationDuration = 0.5f;
    protected int _comboCounter = 0;
    protected float[] _damageValues = new float[3];
    protected float[] _animationDelay = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float hp)
    {
        _health = hp;
    }

    public float getHealth()
    {
        return _health;
    }
    public void takeDamage(float hp)
    {
        _health-=hp;
        if (_health < 0.0f)
        die();
    }

    protected void die()
    {
        if (_health < 0.0f)
        //TODO: Dying animation, loot drops, etc.
        gameObject.SetActive(false);
    }


    protected void setCombo(float x, float y, float z, float u, float v, float w, float r, float s, float t)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        _damageValues[0]=x;
        _damageValues[1]=y;
        _damageValues[2]=z;
        _animationDelay[0]=u;
        _animationDelay[1]=v;
        _animationDelay[2]=w;
        _range[0] = r;
        _range[1] = s;
        _range[2] = t;
    }
}
