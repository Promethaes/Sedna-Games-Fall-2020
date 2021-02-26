using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TextToNetwork : MonoBehaviour
{
    public TMP_InputField field;
    public UDPClient client;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        client.Send(field.text);
        field.text = "";
    }
}
