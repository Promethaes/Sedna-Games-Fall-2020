using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    [Header("Player backend")]
    public PlayerBackend backend = null;

    [Header("Fill mask")]
    public Image fillMask;
    public float maximum = 100.0f;

    // [Range(0.0f, 1000.0f)]
    public float currentFill = 100.0f;

    [Header("Centre images")]
    [SerializeField] private Image centreImage = null;
    [SerializeField] private List<Sprite> images = new List<Sprite>();

    void Start() {
        if(backend == null) {
            Logger.Error("No backend for player health UI!");
            return;
        }

        currentFill = maximum = backend.hp;
        var index = (int)backend.gameObject.GetComponent<PlayerController>().playerType;
        centreImage.sprite = images[index];
    }

    void Update() {
        currentFill = backend.hp;
        fillMask.fillAmount = currentFill / maximum;
    }

}
