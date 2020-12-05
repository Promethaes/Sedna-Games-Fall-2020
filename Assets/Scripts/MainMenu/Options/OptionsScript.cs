using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public List<GameObject> listOfOptions = new List<GameObject>();
    public Dictionary<string, GameObject> options = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var option in listOfOptions)
        {
            options[option.name] = option;
        }
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
            option.Value.SetActive(!toggle);
        }
        toggle = !toggle;
    }
}
