using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerBackend _backend = null;
    public float maximum = 100.0f;

    [Range(0.0f, 100.0f)]
    public float currentFill = 100.0f;

    public Image fillMask;

    void Start()
    {
        if(_backend == null) Debug.LogAssertion("No backend for player health UI!");
        currentFill = maximum = _backend.hp;
    }

    void Update()
    {
        currentFill = _backend.hp;
        fillMask.fillAmount = currentFill / maximum;
    }


}
