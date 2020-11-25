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

    public PlayerConfiguration(PlayerInput playerInput) {
        input = playerInput;
        index = playerInput.playerIndex;
    }
}