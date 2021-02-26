using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothNetworkMovement : MonoBehaviour
{

    public Transform t;
    public Vector3 oldPos;
    public Vector3 newPos;
    public Quaternion oldRot;
    public Quaternion newRot;
    public float lastTime;
    public float thisTime;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        //get proper transform //TODO
    }

    public void updatePos(Vector3 v)
    {
        oldPos = newPos;
        newPos = v;
        lastTime = thisTime;
        thisTime = Time.time;
    }

    public void updateRot(Quaternion q)
    {
        oldRot = newRot;
        newRot = q;
        lastTime = thisTime;
        thisTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time - (thisTime - lastTime);
        float percent = ((currentTime - lastTime) / (thisTime - lastTime));

        t.position = new Vector3(Mathf.Lerp(oldPos.x,newPos.x, percent), Mathf.Lerp(oldPos.y, newPos.y, percent), Mathf.Lerp(oldPos.z, newPos.z, percent));

        t.rotation = Quaternion.Slerp(oldRot, newRot, percent);

    }
}
