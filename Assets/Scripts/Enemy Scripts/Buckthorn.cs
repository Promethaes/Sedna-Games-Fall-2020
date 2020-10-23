using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buckthorn : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setHealth(150.0f);
        setCombo(15.0f, 15.0f, 25.0f, 0.6f, 0.6f, 1.8f, 2.5f, 2.5f, 3.5f);
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
            //TODO: Animation; vine whips, move combo to Enemy for melee and figure out a way to reference the animation for each
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
