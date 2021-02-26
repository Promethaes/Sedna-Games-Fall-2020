using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaShark : MonoBehaviour
{
    public Transform leapTo;
    public Transform leapFrom;
    public Rigidbody rigidBody;
    public float cooldown=2.5f;
    bool _jumpDir = false;
    //Time taken to reach target
    public float arcTime = 1.0f;

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
        float _distance = (leapTo.position-leapFrom.position).magnitude;
        Vector3 _direction = Vector3.Normalize(leapTo.position-leapFrom.position);
        float speed = _distance/arcTime;
        if (_jumpDir)
            _direction = -_direction;

        _direction*=speed;
        _direction.y =24.525f*arcTime/1.9225f;
        rigidBody.useGravity = true;
        rigidBody.velocity = _direction;

        _jumpDir = !_jumpDir;

        yield return new WaitForSeconds(1.0f);
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        //NOTE: Minor y-axis correction due to minor differences in deltaTime and WaitForSeconds
        if (Vector3.Distance(transform.position, leapTo.position) <= 1.0f)
            transform.position = leapTo.position;
        else
            transform.position = leapFrom.position;
    }
}
