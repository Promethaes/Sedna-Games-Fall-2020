using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneNavigationScript : MonoBehaviour
{
    public TMPro.TMP_InputField SIDField;
    public TMPro.TMP_InputField nameField;

    public int roomButtonSID = 9999;
    public int roomOccupents = -1;
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
        //not yet implemented
        UnityEngine.SceneManagement.SceneManager.LoadScene("ViewLobbies");
    }
}
