using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceMonster : MonoBehaviour
{
    public FlamethrowerHitbox flamethrower;
    public float flameSpeed = 2.5f;
    public float flameDamage = 10.0f;

    public void Attack()
    {
        float _attackPattern = Random.Range(0.0f, 1.0f);
        if (_attackPattern < 0.3f)
            StartCoroutine(Flamethrower());
        else
            StartCoroutine(Claw());
    }

    public bool DecideAttack()
    {
        float _attackPattern = Random.Range(0.0f, 1.0f);
        Debug.Log("fire or not: " + (_attackPattern < 0.3f).ToString());
        return _attackPattern < 0.3f;
    }

    public void ActivateClaw()
    {
        StartCoroutine(Claw());
    }
    public void ActivateFlamethrower()
    {
        StartCoroutine(Flamethrower());
    }

    IEnumerator Flamethrower()
    {
        float _lifeTime = 15.0f;
        flamethrower.gameObject.SetActive(true);
        float _angle = 15.0f;
        bool _clockwise = true;
        while (_lifeTime > 0.0f)
        {
            if (flamethrower.transform.parent.rotation.eulerAngles.y > 30.0f && flamethrower.transform.parent.rotation.eulerAngles.y < 45.0f)
                _clockwise = false;
            else if (flamethrower.transform.parent.rotation.eulerAngles.y < 330.0f && flamethrower.transform.parent.rotation.eulerAngles.y > 315.0f)
                _clockwise = true;

            if (_clockwise)
                flamethrower.transform.parent.Rotate(Vector3.up * _angle * flameSpeed * Time.deltaTime);
            else
                flamethrower.transform.parent.Rotate(-1.0f * Vector3.up * _angle * flameSpeed * Time.deltaTime);
            _lifeTime -= Time.deltaTime;
            yield return null;
        }

        flamethrower.gameObject.SetActive(false);

        yield return null;
    }
    IEnumerator Claw()
    {
        //TODO: Do animator calls in here
        GetComponentInParent<EnemyData>().hitbox.SetActive(true);
        yield return null;
    }
}
