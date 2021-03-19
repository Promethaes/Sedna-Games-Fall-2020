using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameScript : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI text;
    GameObject activeCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (playerController.userName == "")
        {
            gameObject.SetActive(false);
            return;
        }
        text.text = playerController.userName;

       activeCamera = GameObject.Find("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(activeCamera.transform);
    }
}
