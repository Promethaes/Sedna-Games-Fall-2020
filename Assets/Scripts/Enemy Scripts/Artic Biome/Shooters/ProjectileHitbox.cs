using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitbox : MonoBehaviour
{
    float _lifeTime = 5.0f;

    //TODO: Depending on what kind of projectile, maybe add an effect instead of just making it disappear
    IEnumerator Shatter()
    {
        yield return null;
    }
    private void Update() 
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0.0f)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerBackend foe = other.GetComponentInParent<PlayerBackend>();
            foe.takeDamage(gameObject.GetComponentInParent<EnemyData>().damageValues);
            if (Random.Range(0.0f,1.0f) <= 0.3f)
                foe.GetComponent<PlayerController>().slowed();
            gameObject.SetActive(false);
        }
    }
}
