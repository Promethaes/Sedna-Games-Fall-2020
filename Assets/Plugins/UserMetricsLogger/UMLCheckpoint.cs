using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMLCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<UserMetricsLoggerScript>().checkpoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
