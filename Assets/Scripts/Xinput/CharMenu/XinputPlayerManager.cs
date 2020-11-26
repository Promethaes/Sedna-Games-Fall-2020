using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XinputPlayerManager : MonoBehaviour
{
    public XinputManager inputManager;
    public List<GameObject> players;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreservePlayers()
    {
        foreach (var player in players)
            DontDestroyOnLoad(player);
    }

}
