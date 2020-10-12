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

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
