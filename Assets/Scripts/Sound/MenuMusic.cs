using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "IntroCutscene")
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        emitter.Stop();
    }
}
