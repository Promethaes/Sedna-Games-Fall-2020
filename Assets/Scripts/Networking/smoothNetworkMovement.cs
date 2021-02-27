using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper
{
    public float lastTime = 0.0f;
    public float thisTime = 0.001f;
}

public class smoothNetworkMovement : MonoBehaviour
{

    public Transform t;
    public Vector3 oldPos;
    public Vector3 newPos;
    public Quaternion oldRot;
    public Quaternion newRot;

    TimeKeeper posTime = null;
    TimeKeeper rotTime = null;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();

        posTime = new TimeKeeper();
        rotTime = new TimeKeeper();

        //get proper transform //TODO
    }

    public void updatePos(Vector3 v)
    {
        oldPos = newPos;
        newPos = v;
        posTime.lastTime = posTime.thisTime;
        posTime.thisTime = Time.time;
    }

    public void updateRot(Quaternion q)
    {
        oldRot = newRot;
        newRot = q;
        rotTime.lastTime = rotTime.thisTime;
        rotTime.thisTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        _UpdatePos();
        _UpdateRot();

    }

    float u = 0.0f;
    void _UpdatePos()
    {
        float currentTime = Time.time - (posTime.lastTime);
        float percent = ((currentTime - posTime.lastTime) / (posTime.thisTime - posTime.lastTime));

        if (u >= 1.0f)
        {
            u = 0.0f;
            oldPos = newPos;
        }
        if (oldPos != newPos)
            u += Time.deltaTime;


        t.position = new Vector3(Mathf.Lerp(oldPos.x, newPos.x, u), Mathf.Lerp(oldPos.y, newPos.y, u), Mathf.Lerp(oldPos.z, newPos.z, u));
    }

    float rotU = 0.0f;
    void _UpdateRot()
    {
        float currentTime = Time.time - (rotTime.lastTime);
        float percent = ((currentTime - rotTime.lastTime) / (rotTime.thisTime - rotTime.lastTime));

        if (rotU >= 1.0f)
        {
            rotU = 0.0f;
            oldRot = newRot;
        }
        if (oldRot != newRot)
            rotU += Time.deltaTime;

        t.rotation = Quaternion.Slerp(oldRot, newRot, rotU);
    }
}
