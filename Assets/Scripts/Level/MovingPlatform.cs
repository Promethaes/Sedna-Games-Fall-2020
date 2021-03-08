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
        Vector3 dir = target.position-transform.position;
        if ((transform.position - target.position).magnitude < 0.5f && !pause)
        {
            pause = true;
            if (target == start)
                target = end;
            else
                target = start;
            StartCoroutine(StopPlatform());
        }
        else if (!pause)
        {
            transform.position += dir.normalized*Time.deltaTime*speed;
            if (player!=null)
                player.transform.position += dir.normalized*Time.deltaTime*speed;;
        }
    }
    IEnumerator StopPlatform()
    {
        yield return new WaitForSeconds(1.5f);
        pause = false;
    }
    private void OnCollisionStay(Collision other) {
        player = other.transform;
    }
    private void OnCollisionExit(Collision other) {
        player = null;
    }
}
