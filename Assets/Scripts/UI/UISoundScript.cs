using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
//https://answers.unity.com/questions/921720/how-can-i-check-if-a-ui-button-is-selected.html
public class UISoundScript : MonoBehaviour, ISelectHandler
{
    public StudioEventEmitter emitter;
    public StudioEventEmitter uiClickSound;
    public void OnSelect(BaseEventData eventData)
    {
        emitter.Play();
    }
    public void OnClick()
    {
        uiClickSound.Play();
    }
}
