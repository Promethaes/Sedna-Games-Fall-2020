using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerToList : MonoBehaviour
{
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager.players.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
