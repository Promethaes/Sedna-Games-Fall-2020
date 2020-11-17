using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButtonScript : MenuButtonFunctionality
{
    public override void execute()
    {
        GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeScene(1);
    }

}
