using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinLobbyScript : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;

    public void JoinLobby()
    {
        if (inputField.text != "")
        {
            PlayerPrefs.SetString("SID", inputField.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("CSNetLobby");
        }
    }

}
