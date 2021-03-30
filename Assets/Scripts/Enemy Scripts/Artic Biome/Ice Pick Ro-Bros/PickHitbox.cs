using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerBackend foe = other.GetComponent<PlayerBackend>();
            foe.takeDamage(gameObject.GetComponentInParent<EnemyData>().damageValues);
            if (Random.Range(0.0f,1.0f) <= 0.3f)
                foe.GetComponent<PlayerController>().slowed();
            gameObject.SetActive(false);
        }
    }
}
