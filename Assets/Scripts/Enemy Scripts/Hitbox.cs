using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerBackend foe = other.GetComponentInParent<PlayerBackend>();
            foe.hp -= gameObject.GetComponentInParent<EnemyData>().damageValues;
            gameObject.SetActive(false);
        }
    }
}
