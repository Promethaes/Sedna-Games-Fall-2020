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
            if (player.venomBuff)
                foe.Poison();
            if (foe.health <= 0.0f)
                player.killCount++;
            player.hitEnemy = true;
        }
    }
}
