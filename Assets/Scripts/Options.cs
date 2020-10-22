using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Options : MonoBehaviour
{

    //@Temporary: only for debugging. Will delete later
    public bool lockCursor = true;

    public int numPlayers = 1;

    bool enableCouchCoop = true; // @Cleanup? Editor warnings are annoying...
    int _lastNumPlayers = 0;   // @Cleanup?

    void Start() {}

    void Update() {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
