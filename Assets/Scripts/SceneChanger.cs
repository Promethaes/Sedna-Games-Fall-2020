﻿using UnityEngine;
using UnityEngine.SceneManagement;

//
// Can't make any of this static because otherwise Unity can't handle it in button click events...reeeeeee
// 
public class SceneChanger : MonoBehaviour {
    public void changeScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void quitApp() {
        // @Cleanup: this is here because the editor ignores application quit requests, at least with this log we know that it works
        Debug.LogWarning("Application quit");
        Application.Quit();
    }
}
