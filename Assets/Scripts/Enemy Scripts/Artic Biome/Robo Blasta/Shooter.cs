using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] projectiles;
    public Transform muzzle;

    public float projectileSpeed = 1.0f;

    public void Attack()
    {
        StartCoroutine(Shoot());
    }
    public void Charge()
    {
        StartCoroutine(ChargeShot());
    }

    IEnumerator Shoot()
    {
        //TODO: Figure out how to instantiate with attached scripts
        for (int i=0;i<3;i++)
        {
            var bullet = Instantiate(projectiles[0]);
            bullet.transform.position = muzzle.position;
            bullet.gameObject.SetActive(true);
            bullet.transform.LookAt(muzzle.position+muzzle.forward);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }

    IEnumerator ChargeShot()
    {
        var bullet = Instantiate(projectiles[1]);
        bullet.transform.position = muzzle.position;
        bullet.gameObject.SetActive(true);
        bullet.transform.LookAt(transform.position+transform.forward);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        yield return null;
    }
}
