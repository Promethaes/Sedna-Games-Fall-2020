using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Wet", "true");
        PlayerPrefs.SetString("Arctic", "false");
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnToggle(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("Wet", "false");
            PlayerPrefs.SetString("Arctic", "true");
        }
        else
        {
            PlayerPrefs.SetString("Wet", "true");
            PlayerPrefs.SetString("Arctic", "false");
        }

        PlayerPrefs.Save();
    }
}
