using UnityEngine;
using UnityEngine.SceneManagement;

//
// Can't make any of this static because otherwise Unity can't handle it in button click events...reeeeeee
// 
public class SceneChanger : MonoBehaviour
{
    public void changeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void onStartButtonPressed()
    {
        var optionsScript = FindObjectOfType<OptionsScript>();
        if (UseXinputScript.use)
            changeScene(3);
        else
            changeScene(1);
    }

    public void onOnlineButtonPressed()
    {
        changeScene(2);
    }

    public void quitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
