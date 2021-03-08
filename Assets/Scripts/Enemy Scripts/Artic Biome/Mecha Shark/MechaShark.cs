using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaShark : MonoBehaviour
{
    public Transform leapTo;
    public Transform leapFrom;
    public Rigidbody rigidBody;
    Transform target;
    public float cooldown=2.5f;
    //Time taken to reach target
    public float arcTime = 2.0f;
    private void Start() {
        target = leapTo;
        GetComponent<EnemyData>().billboard.gameObject.SetActive(false);
    }

    private void Update() 
    {
        Leap();
        cooldown-=Time.deltaTime;    
    }
    //NOTE: Need more information. Is it statically leaping between two spots or following the player and leaping?
    public void Leap()
    {
        if (cooldown <= 0.0f)
        {
            StartCoroutine(_Leap());
            cooldown = 5.0f;
            }
    }

    IEnumerator _Leap()
    {
        var _hitbox = GetComponent<EnemyData>().hitbox;
        _hitbox.SetActive(true);
        float _distance = (target.position-transform.position).magnitude;
        Vector3 dir = (target.position-transform.position).normalized;
        float speed = _distance/arcTime;
        dir*=speed;
        dir.y =24.525f*arcTime/1.9225f;
        rigidBody.useGravity = true;
        rigidBody.velocity = dir;

        yield return new WaitForSeconds(arcTime);
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        transform.position = target.position;
        //NOTE: Minor y-axis correction due to minor differences in deltaTime and WaitForSeconds
        if ((target.position-leapTo.position).magnitude <= 1.0f)
            target = leapFrom;
        else
            target = leapTo;
        _hitbox.SetActive(false);
    }
}
