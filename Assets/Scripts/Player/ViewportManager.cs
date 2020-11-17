using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     * @brief: Places, Scales and Activates all viewports according to the number of players currently active.
     * @param: number of players
     * @param: list of each player's camera
     */
    public void SpaceViewportsAppropriately(int numPlayers,List<Camera> cameras)
    {
        foreach(Camera camera in cameras)
            camera.rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f));

        if(numPlayers == 1)
        {
            cameras[0].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f));
        }
        else if (numPlayers == 2)
        {
            cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f));
            cameras[1].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.5f));
        }
        else if (numPlayers == 3)
        {
            cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f));
            cameras[1].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f));
            cameras[2].rect = new Rect(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 0.5f));
        }
        else if (numPlayers == 4)
        {
            cameras[0].rect = new Rect(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.5f));
            cameras[1].rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            cameras[2].rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f));
            cameras[3].rect = new Rect(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 0.5f));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
