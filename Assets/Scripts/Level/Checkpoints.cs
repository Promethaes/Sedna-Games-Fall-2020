using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: Only apply this script to Checkpoints beyond the first (i.e. not 0)
public class Checkpoints : MonoBehaviour
{
    public CheckpointManager manager;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            if (manager.checkSpawn() != transform)
                manager.newSpawn();
        }
    }
}
