using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextToUsername : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        var name = PlayerPrefs.GetString("pid", "");
        if (name == "")
            return;
        text.text = name;
    }

    public void DisplayName()
    {
        var name = PlayerPrefs.GetString("pid", "");
        if (name == "")
            return;
        text.text = name;
    }
}
