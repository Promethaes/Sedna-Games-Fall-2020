using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public GameObject attack;
    public GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            PlayerController player = GetComponentInParent<PlayerController>();
            EnemyData foe = other.GetComponentInParent<EnemyData>();
            Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
            Instantiate(hitEffect, closestPoint, Quaternion.identity);
            hitEffect.GetComponent<DestroyVFX>().destroyVFX(0.5f);
            foe.takeDamage(player.damageValues[player.comboCounter]);
            if (player.venomBuff)
                foe.Poison();
            if (foe.health <= 0.0f)
                player.killCount++;
            player.hitEnemy = true;
        }
    }
}
