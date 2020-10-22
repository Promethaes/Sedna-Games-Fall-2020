using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setHealth(120.0f);
        setCombo(8.0f, 12.0f, 20.0f, 0.8f, 1.2f, 1.7f, 0.9f, 1.0f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        _animationDuration -= Time.deltaTime;
        Combo();
    }
    void Combo()
    {
        if (_animationDuration < 0.0f)
        {
            _animationDuration = _animationDelay[_comboCounter];
            //TODO: Animation; heavy sludge attacks(?)
            RaycastHit enemy;
            if (Physics.Raycast(transform.position, transform.forward, out enemy, _range[_comboCounter]) && enemy.transform.tag == "Player")
            {
           ///    PlayerInputScript foe = enemy.collider.GetComponentInParent<PlayerInputScript>();
           ///    foe.takeDamage(_damageValues[_comboCounter]);
           ///    Debug.Log(foe.getHealth());
            }
            _comboCounter++;
            if (_comboCounter > 2)
                _comboCounter = 0;
        }
    }
}
