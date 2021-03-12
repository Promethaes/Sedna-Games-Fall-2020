using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraScript : MonoBehaviour
{
    public static GameObject _player;
    private bool set = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    void Update()
    {
        if (_player != null&&!set)
        {
            Cinemachine.CinemachineVirtualCamera camera = gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();

            camera.LookAt = _player.transform;
            camera.Follow = _player.transform;
            camera.enabled = true;
            set = true;
        }
    }

}
