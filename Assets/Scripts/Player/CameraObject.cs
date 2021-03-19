using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
   static public SkinnedMeshRenderer player;
   static public Cinemachine.CinemachineVirtualCamera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("virtualCam");
        myCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    private void Awake()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("virtualCam");
        myCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = player.transform.position;
        //gameObject.transform.rotation = player.transform.rotation;
        //gameObject.transform.Rotate(new Vector3(0,1,0),180);
        
    }
    static public  void ChangeSkinnedMesh(SkinnedMeshRenderer newRenderer)
    {
        myCamera.m_LookAt = newRenderer.transform;
        myCamera.m_Follow = newRenderer.transform;
    }
}
