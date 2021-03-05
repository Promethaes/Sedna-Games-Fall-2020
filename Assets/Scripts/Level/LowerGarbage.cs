using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerGarbage : MonoBehaviour
{
    public bool areaComplete = false;
    private Transform t;
    private float sinkTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (areaComplete)
        {
            sinkTime += Time.deltaTime;
            t.position = new Vector3(t.position.x,Mathf.Lerp(t.position.y,-50,sinkTime/6),t.position.z);
            if (sinkTime >= 6)
            {
                gameObject.SetActive(false);
            }
        } 
    }
}
