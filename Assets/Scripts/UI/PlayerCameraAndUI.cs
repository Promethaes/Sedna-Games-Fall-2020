using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAndUI : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform playerUIPanel;

    public void setCameraRect(Rect cameraRect) {
        setCameraRect(cameraRect.x, cameraRect.y, cameraRect.width, cameraRect.height);
    }

    public void setCameraRect(float x0, float y0, float x1, float y1) {
        var sizeDelta = playerUIPanel.sizeDelta;
        playerUIPanel.sizeDelta = new Vector2(Screen.width, Screen.height);

        mainCamera.rect = new Rect(x0, y0, x1, y1);
        playerUIPanel.localScale = new Vector2(x1, y1);

        var newX = x0 * playerUIPanel.rect.width;
        var newY = y0 * playerUIPanel.rect.height;

        playerUIPanel.position = new Vector3(newX, newY);
        playerUIPanel.sizeDelta = sizeDelta;
        Logger.Log("Player UI Panel position after: {0}", playerUIPanel.position);
    }
}
