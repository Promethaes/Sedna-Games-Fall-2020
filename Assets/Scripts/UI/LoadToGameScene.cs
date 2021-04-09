using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToGameScene : MonoBehaviour
{
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }
}
