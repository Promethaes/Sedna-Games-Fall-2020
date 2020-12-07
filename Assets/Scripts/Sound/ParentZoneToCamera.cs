using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentZoneToCamera : MonoBehaviour
{
    private void Start()
    {
        var c = FindObjectOfType<Camera>();
        c.transform.localScale = Vector3.one;
        gameObject.transform.SetParent(c.transform);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.localPosition = Vector3.zero;
    }
}
