using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerBackend foe = other.GetComponentInParent<PlayerBackend>();
            foe.takeDamage(gameObject.GetComponentInParent<FurnaceMonster>().flameDamage);
            if (Random.Range(0.0f,1.0f) <= 0.3f)
                foe.GetComponent<PlayerController>().poisoned();
        }
    }
}
