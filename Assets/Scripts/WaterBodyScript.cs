using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBodyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharMenuInput>())
        {
            if(collision.gameObject.GetComponent<CharMenuInput>().playerType != 3)
            {
                //call die or something, TODO
                collision.gameObject.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
            }
        }
    }

}
