using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    static public SkinnedMeshRenderer player;
    static public Cinemachine.CinemachineVirtualCamera myCamera;
    public GameObject cameraObject;
    static private bool changed = false;

    [SerializeField]
    private float UpDownSpeed = 0.25f;
    private float UpDownCurrent = 0.0f;
    private float UpMax = 5.0f;
    private float DownMax = -5.0f;


    private void Awake()
    {
        myCamera = cameraObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    public void MoveUpDown(float upDown)
    {
        
        // transform.Rotate(new Vector3(0,0,1), UpDownCurrent);
        float updownAfterCalc = upDown * UpDownSpeed;
        if (updownAfterCalc > 0 && UpDownCurrent + updownAfterCalc < UpMax) 
        {
             UpDownCurrent += updownAfterCalc;
             transform.Translate(new Vector3(0, updownAfterCalc, 0));          
        }
        else if (updownAfterCalc < 0 && UpDownCurrent + updownAfterCalc > DownMax)
        {
              UpDownCurrent += updownAfterCalc;
              transform.Translate(new Vector3(0, updownAfterCalc, 0));
        }
           
    }
    static public void ChangeSkinnedMesh(SkinnedMeshRenderer newRenderer)
    {
        if (!myCamera)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("virtualCam");
            myCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }
        myCamera.m_LookAt = newRenderer.transform;
        player = newRenderer;
        changed = true;
    }
    public void changeParent()
    {
        if (changed)
        {
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0, 0, 0);
            changed = false;
        }

    }
    private void Update()
    {
        changeParent();
    }
}
