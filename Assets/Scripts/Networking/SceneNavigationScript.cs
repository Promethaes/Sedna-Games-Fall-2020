using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneNavigationScript : MonoBehaviour
{
    public TMPro.TMP_InputField SIDField;
    public TMPro.TMP_InputField nameField;

    public TMPro.TMP_InputField ipField;

    public int roomButtonSID = 9999;
    public int roomOccupents = -1;

    private void Start()
    {
        if (ipField)
            ipField.text = PlayerPrefs.GetString("serverIP", "127.0.0.1");
    }

    public void JoinLobby()
    {
        if (SIDField.text != "")
        {
            PlayerPrefs.SetString("SID", SIDField.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("CSNetLobby");
        }
    }

    public void ListJoinLobby()
    {
        PlayerPrefs.SetString("SID", roomButtonSID.ToString());
        UnityEngine.SceneManagement.SceneManager.LoadScene("CSNetLobby");
    }

    public void SavePlayerName()
    {
        if (nameField.text == "")
            return;
        Debug.Log(nameField.text);
        PlayerPrefs.SetString("pid", nameField.text);
    }

    public void ChangeToOnlineMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OnlineMenu");
    }

    public void ChangeToNetLobby()
    {
        PlayerPrefs.SetString("SID", "-1");
        UnityEngine.SceneManagement.SceneManager.LoadScene("CSNetLobby");
    }

    public void ChangeToLobbyView()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ViewLobbies");
    }
    public void ChangeToLeaderboards()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LeaderboardScene");
    }
    public void ChangeToStartMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu2");
    }

    public void SetServerIP(string ip)
    {
        PlayerPrefs.SetString("serverIP", ip);
        Debug.Log("Set Server IP to " + ip);
    }
}
