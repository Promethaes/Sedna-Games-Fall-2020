using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButtonScript : MenuButtonFunctionality
{
    public override void execute()
    {
        var sceneChanger = FindObjectOfType<SceneChanger>();
        sceneChanger.quitApp();
    }

}
