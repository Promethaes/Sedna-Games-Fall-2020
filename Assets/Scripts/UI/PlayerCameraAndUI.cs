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
        var invAspectRatio = (float)(Screen.height) / (float)(Screen.width);
        playerUIPanel.sizeDelta = new Vector2(Screen.width, Screen.height);

        mainCamera.rect = new Rect(x0, y0, x1, y1);
        playerUIPanel.localScale = new Vector2(y1, y1);

        // Kinda @Cursed @Ugh
        var newX = ((x1 != x0 ? 1 / (x1 - x0) : 0) * -0.25f + 0.5f) * (y1 > 0.5f ? 0 : 1) * Screen.width;
        var newY = y0 * playerUIPanel.rect.height;

        playerUIPanel.position = new Vector3(newX, newY);
        playerUIPanel.sizeDelta = sizeDelta;
    }
}
