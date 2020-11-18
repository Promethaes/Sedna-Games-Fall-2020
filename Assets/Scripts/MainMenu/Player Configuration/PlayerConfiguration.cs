using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType {
    BISON,
    POLAR_BEAR,
    RATTLESNAKE,
    TURTLE,
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