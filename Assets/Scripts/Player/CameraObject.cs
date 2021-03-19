using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    static public SkinnedMeshRenderer player;
    static public Cinemachine.CinemachineVirtualCamera myCamera;
    public GameObject cameraObject;

    private void Awake()
    {
        myCamera = cameraObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    static public void ChangeSkinnedMesh(SkinnedMeshRenderer newRenderer)
    {
        if (!myCamera)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("virtualCam");
            myCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }
        myCamera.m_LookAt = newRenderer.transform;
        myCamera.m_Follow = newRenderer.transform;
    }
}
