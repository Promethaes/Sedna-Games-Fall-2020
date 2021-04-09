using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GoToNextScene : MonoBehaviour {
    [SerializeField] VideoPlayer _videoPlayer = null;

    // Update is called once per frame
    void Update() {
        if(_videoPlayer.frame <= 0) return;
        if(_videoPlayer.isPlaying != false) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
