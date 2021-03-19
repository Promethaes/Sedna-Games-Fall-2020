using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ProjectileHitbox[] projectiles;

    public float projectileSpeed = 1.0f;

    public void Attack()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        for (int i=0;i<projectiles.Length;i++)
        {
            projectiles[i].gameObject.SetActive(true);
            //TODO: Adjust this for actual gun location, probably through a transform located at muzzle
            projectiles[i].transform.position = GetComponent<EnemyData>().hitbox.transform.position;
            projectiles[i].GetComponent<Rigidbody>().AddForce(projectiles[i].transform.forward.normalized * projectileSpeed,ForceMode.Impulse);
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }
}
