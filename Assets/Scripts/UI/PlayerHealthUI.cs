using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlayerHealthUI : MonoBehaviour
{
    [Header("Player backend")]
    [SerializeField] private PlayerBackend _backend = null;

    [Header("Fill mask")]
    public Image fillMask;
    public float maximum = 100.0f;

    [Range(0.0f, 100.0f)]
    public float currentFill = 100.0f;

    [Header("Centre images")]
    [SerializeField] private Image centreImage = null;
    [SerializeField] private List<Sprite> images = new List<Sprite>();

    void Start()
    {
        if(_backend == null) Debug.LogAssertion("No backend for player health UI!");
        currentFill = maximum = _backend.hp;
        var index = _backend.gameObject.GetComponent<CharMenuInput>().playerType - 1;
        centreImage.sprite = images[index];
    }

    void Update()
    {
        currentFill = _backend.hp;
        fillMask.fillAmount = currentFill / maximum;
    }

}
