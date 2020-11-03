using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPlayerManager : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(375.0f, 25.0f, 240.0f);
    public List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
    }

    public void PreservePlayers()
    {
        foreach (var player in players)
        {
            Object.DontDestroyOnLoad(player);
            player.GetComponent<CharMenuInput>().Cleanup();
            player.gameObject.transform.position = spawnPosition + new Vector3(player.GetComponent<CharMenuInput>().playerType,0.0f,0.0f);
        }

        gameObject.GetComponent<PlayerManager>().players = players;
        gameObject.GetComponent<PlayerManager>().enabled = true;
        gameObject.GetComponent<PlayerInputManager>().enabled = false;
       
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    
}
