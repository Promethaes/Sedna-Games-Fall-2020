using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Options : MonoBehaviour
{

    //@Temporary: only for debugging. Will delete later
    public bool lockCurser = true;

    public int numPlayers = 1;

    bool enableCouchCoop = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    int _lastNumPlayers = 0;
    // Update is called once per frame
    void Update()
    {
        if (lockCurser)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

    }
}
