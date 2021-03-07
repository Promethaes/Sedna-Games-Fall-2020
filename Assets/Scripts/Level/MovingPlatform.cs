using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start;
    public Transform end;
    bool pause = false;
    public float speed = 5.0f;
    Transform target;
    Transform player;
    private void Start() 
    {
        target = end;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position-target.position;
        if ((transform.position - target.position).magnitude < 0.5f && !pause)
        {
            pause = true;
            if (target == start)
                target = end;
            else
                target = start;
            float timer = 1.5f;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            pause = false;
        }
        else
        {
            transform.position += dir.normalized*Time.deltaTime*speed;
            player.transform.position += dir.normalized*Time.deltaTime*speed;;
        }
    }
    private void OnTriggerStay(Collider other) {
        player = other.transform;
    }
}
