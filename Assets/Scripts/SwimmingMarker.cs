using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PolarBearScript>().swimPositionMarkers.Add(this.gameObject);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
