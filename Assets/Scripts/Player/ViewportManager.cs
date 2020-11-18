using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour {
    /*
     * @brief: Places, Scales and Activates all viewports according to the number of players currently active.
     * @param: number of players
     * @param: list of each player's camera
     */
    public void SpaceViewportsAppropriately(int numPlayers, List<PlayerCameraAndUI> cameras) {
        foreach(PlayerCameraAndUI camera in cameras) {
            camera.setCameraRect(0.0f, 0.0f, 0.0f, 0.0f);
        }

        switch(numPlayers) {
            case 1:
                cameras[0].setCameraRect(0.0f, 0.0f, 1.0f, 1.0f);
                break;
            case 2:
                cameras[0].setCameraRect(0.0f, 0.5f, 1.0f, 0.5f);
                cameras[1].setCameraRect(0.0f, 0.0f, 1.0f, 0.5f);
                break;
            case 3:
                cameras[0].setCameraRect(0.0f, 0.5f, 1.0f, 0.5f);
                cameras[1].setCameraRect(0.0f, 0.0f, 0.5f, 0.5f);
                cameras[2].setCameraRect(0.5f, 0.0f, 0.5f, 0.5f);
                break;
            case 4:
                cameras[0].setCameraRect(0.0f, 0.5f, 0.5f, 0.5f);
                cameras[1].setCameraRect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[2].setCameraRect(0.0f, 0.0f, 0.5f, 0.5f);
                cameras[3].setCameraRect(0.5f, 0.0f, 0.5f, 0.5f);
                break;
            default:
                break;
        }

        // @Cleanup @Cleanup @Cleanup
        // if(numPlayers == 1)
        // {
        //     cameras[0].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f));
        // }
        // else if (numPlayers == 2)
        // {
        //     cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f));
        //     cameras[1].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.5f));
        // }
        // else if (numPlayers == 3)
        // {
        //     cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f));
        //     cameras[1].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f));
        //     cameras[2].rect = new Rect(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 0.5f));
        // }
        // else if (numPlayers == 4)
        // {
        //     cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.5f));
        //     cameras[1].rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        //     cameras[2].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f));
        //     cameras[3].rect = new Rect(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 0.5f));
        // }
    }
}
