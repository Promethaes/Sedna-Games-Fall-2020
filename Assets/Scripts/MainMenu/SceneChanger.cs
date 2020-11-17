using UnityEngine;
using UnityEngine.SceneManagement;

//
// Can't make any of this static because otherwise Unity can't handle it in button click events...reeeeeee
// 
public class SceneChanger : MonoBehaviour {
    public void changeScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void quitApp() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
