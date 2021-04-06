using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitbox : MonoBehaviour
{
    float _lifeTime = 5.0f;

    private void Update() 
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0.0f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerBackend foe = other.GetComponent<PlayerBackend>();
            //foe.takeDamage(gameObject.GetComponentInParent<EnemyData>().damageValues);
            //NOTE: Bullets don't have a parent so can't get enemydata
            foe.takeDamage(10.0f);
            if (Random.Range(0.0f,1.0f) <= 0.3f)
                foe.GetComponent<PlayerController>().poisoned();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (other.tag == "Terrain")
            Destroy(gameObject);
    }
}
