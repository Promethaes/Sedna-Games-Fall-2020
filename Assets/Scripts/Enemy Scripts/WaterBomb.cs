using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    float _damage;
    private void Start() 
    {
    }

    public void setDamage(float dmg)
    {
        _damage = dmg;
    }
    private void Update() 
    {
        Explode();
    }
    void Explode()
    {
        RaycastHit enemy;
        if (Physics.SphereCast(transform.position, 5.0f, transform.forward, out enemy, 5.0f) && transform.position.y <= enemy.transform.position.y)
            {
                if (enemy.transform.tag == "Player")
                {
               /// PlayerInputScript foe = enemy.collider.GetComponentInParent<PlayerInputScript>();
               /// foe.takeDamage(_damage);
               /// Debug.Log(foe.getHealth());
                }
                this.gameObject.SetActive(false);
            }   
    }    
}
