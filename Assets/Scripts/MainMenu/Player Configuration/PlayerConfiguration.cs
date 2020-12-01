using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType {
    
    TURTLE,
    RATTLESNAKE,
    POLAR_BEAR,
    BISON,
}

public class PlayerConfiguration {
    public PlayerInput input;
    public PlayerTypeToGameObject character;
    public int index;
    public bool isReady;
    public XinputGamepad gamepad;
    public GameObject gameObject;
    

    public PlayerConfiguration(PlayerInput playerInput) {
        input = playerInput;
        index = playerInput.playerIndex;
    }

    public PlayerConfiguration(XinputGamepad pad, GameObject go){
        gamepad = pad;
        index = gamepad.index;
        gameObject = go;
    }
}