using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public Transform pivot;
    public float cloudMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivot.position, Vector3.up, cloudMoveSpeed * Time.deltaTime);
    }
}
