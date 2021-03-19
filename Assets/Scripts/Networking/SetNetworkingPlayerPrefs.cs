using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SetNetworkingPlayerPrefs : MonoBehaviour, IMoveHandler,ISelectHandler
{
    public TMPro.TMP_InputField inputField;
    public void SetServerIP()
    {
        Debug.Log(inputField.text);
        PlayerPrefs.SetString("ip", inputField.text);
        PlayerPrefs.Save();
    }

    public void OnMove(AxisEventData e)
    {
        Debug.Log("fuck");
    }

    public void OnSelect(BaseEventData e){
        Debug.Log("shit");
    }

}
