using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            PlayerController player = GetComponentInParent<PlayerController>();
            EnemyData foe = other.GetComponentInParent<EnemyData>();
            foe.takeDamage(25.0f);

            //Knockback
            var dir = Vector3.Normalize(foe.transform.position - transform.position);
            foe.GetComponentInParent<Rigidbody>().AddForce(Secrets.limitKnockBack(dir*100.0f), ForceMode.Impulse);
            player.hitEnemy = true;
        }
    }
}
