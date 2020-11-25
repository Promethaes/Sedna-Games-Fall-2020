using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public List<GameObject> options = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    bool toggle = false;
    public void Toggle()
    {
        foreach (var option in options)
        {
            option.SetActive(!toggle);
        }
        toggle = !toggle;
    }
}
