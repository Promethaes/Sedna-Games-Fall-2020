using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothNetworkMovement : MonoBehaviour
{

    Transform t;
    Vector3 oldPos;
    Vector3 newPos;
    List<Vector3> oldPosList = new List<Vector3>();
    List<Vector3> newPosList = new List<Vector3>();
    Quaternion oldRot;
    Quaternion newRot;
    List<Quaternion> oldRotList = new List<Quaternion>();
    List<Quaternion> newRotList = new List<Quaternion>();

    public float lerpSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();

        //get proper transform //TODO
    }

    public void updatePos(Vector3 v)
    {
        oldPos = newPos;
        oldPosList.Add(oldPos);
        newPos = v;
        newPosList.Add(newPos);
    }

    public void updateRot(Quaternion q)
    {
        oldRot = newRot;
        oldRotList.Add(oldRot);
        newRot = q;
        newRotList.Add(newRot);
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
        if (u >= 1.0f)
        {
            u = 0.0f;
            //oldPos = newPos;
            oldPosList.RemoveAt(0);
            newPosList.RemoveAt(0);
        }
        if (newPosList.Count == 0)
            return;

        u += Time.deltaTime*lerpSpeed;

        t.position = Vector3.Lerp(oldPosList[0], newPosList[0], u);
    }

    float rotU = 0.0f;
    void _UpdateRot()
    {
        if (rotU >= 1.0f)
        {
            rotU = 0.0f;
            oldRotList.RemoveAt(0);
            newRotList.RemoveAt(0);
        }
        if (newRotList.Count == 0)
            return;

        rotU += Time.deltaTime*lerpSpeed;

        t.rotation = Quaternion.Slerp(oldRotList[0], newRotList[0], rotU);
    }
}
