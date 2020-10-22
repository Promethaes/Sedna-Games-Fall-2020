using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneAfterThreeSecsScript : MonoBehaviour
{
    float _changeAfter = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _changeAfter -= Time.deltaTime;
        if (_changeAfter <= 0.0f) 
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeScene(0);

    }
}
