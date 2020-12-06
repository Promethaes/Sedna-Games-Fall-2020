using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UMLCheckpoint : MonoBehaviour
{
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" || triggered)
            return;
        FindObjectOfType<UserMetricsLoggerScript>().csLogCheckpointTime(Time.time);
        triggered = true;
    }


}
