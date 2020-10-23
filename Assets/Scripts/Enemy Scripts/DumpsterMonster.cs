using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DumpsterMonster : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setHealth(250.0f);
        setCombo(20.0f, 20.0f, 35.0f, 1.5f, 1.5f, 2.7f, 0.8f, 1.1f, 1.3f);
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
                PlayerBackend foe = enemy.collider.GetComponentInParent<PlayerBackend>();
                foe.hp -= _damageValues[_comboCounter];
            }
            _comboCounter++;
            if (_comboCounter > 2)
                _comboCounter = 0;
        }
    }
}
