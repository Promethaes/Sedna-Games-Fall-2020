using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            PlayerController player = GetComponentInParent<PlayerController>();
            EnemyData foe = other.GetComponentInParent<EnemyData>();
            foe.takeDamage(player.damageValues[player.comboCounter]);
            player.hitEnemy = true;
            gameObject.SetActive(false);
        }
    }
}
