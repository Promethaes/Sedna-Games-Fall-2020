using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPlayerManager : MonoBehaviour
{
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
