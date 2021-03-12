using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToGameScene : MonoBehaviour
{

    public void ChangeToLobbyScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyList");
    }

    public void ChangeToNetLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CSNetLobby");
    }
}
