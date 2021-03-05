using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ProjectileHitbox[] projectiles;

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
            projectiles[i].transform.position = transform.position + transform.forward * 0.5f;
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }
}
