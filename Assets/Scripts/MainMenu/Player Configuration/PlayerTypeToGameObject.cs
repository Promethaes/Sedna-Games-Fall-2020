using UnityEngine;

[System.Serializable]
public class PlayerTypeToGameObject {
    public PlayerType type;
    public GameObject prefab;
}

[System.Serializable]
public class PlayerTypeTo<T> {
    public PlayerType type;
    public T value;
}