using System.Collections.Generic;
using UnityEngine;

public class CameraSplitter : MonoBehaviour {

    [SerializeField] private List<PlayerCameraAndUI> playerCameras = new List<PlayerCameraAndUI>();
    private bool _updateCameras = false;

    // This should be @Cleanup but is useful for debugging purposes
    public void setUpdateFlag() { _updateCameras = true; }

    public void addCameras(PlayerCameraAndUI cameras) {
        playerCameras.Add(cameras);
        _updateCameras = true;
    }

    private void Update() {
        if(!_updateCameras) return;

        switch(playerCameras.Count) {
            case 0:
                Logger.Log("No cameras to resize on screen!");
                break;
            case 1:
                playerCameras[0].setCameraRect(0.0f, 0.0f, 1.0f, 1.0f);
                break;
            case 2:
                playerCameras[0].setCameraRect(0.0f, 0.5f, 1.0f, 0.5f);
                playerCameras[1].setCameraRect(0.0f, 0.0f, 1.0f, 0.5f);
                break;
            case 3:
                playerCameras[0].setCameraRect(0.0f, 0.5f, 1.0f, 0.5f);
                playerCameras[1].setCameraRect(0.0f, 0.0f, 0.5f, 0.5f);
                playerCameras[2].setCameraRect(0.5f, 0.0f, 0.5f, 0.5f);
                break;
            case 4:
                playerCameras[0].setCameraRect(0.0f, 0.5f, 0.5f, 0.5f);
                playerCameras[1].setCameraRect(0.5f, 0.5f, 0.5f, 0.5f);
                playerCameras[2].setCameraRect(0.0f, 0.0f, 0.5f, 0.5f);
                playerCameras[3].setCameraRect(0.5f, 0.0f, 0.5f, 0.5f);
                break;
            default:
                Logger.Log("Attempt fit more than 4 cameras on screen!");
                break;
        }

        _updateCameras = false;
    }
}
