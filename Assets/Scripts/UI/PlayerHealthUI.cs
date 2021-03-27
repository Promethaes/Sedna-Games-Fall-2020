using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Player backend")]
    public PlayerBackend backend = null;

    [Header("Fill mask")]
    public Image fillMask;
    public float maximum = 100.0f;

    public float currentFill = 100.0f;

    [Header("Centre images")]
    [SerializeField] private Image centreImage = null;
    [SerializeField] private List<Sprite> images = new List<Sprite>();

    public void setCharacterImage(int character)
    {
        centreImage.sprite = images[character];
    }

    void Start()
    {
        if (backend == null)
        {
            Logger.Error("No backend for player health UI!");
            return;
        }

        if (backend.gameObject.GetComponent<PlayerController>().remotePlayer)
        {
            gameObject.SetActive(false);
            return;
        }

        currentFill = maximum = backend.maxHP;
        var index = (int)backend.gameObject.GetComponent<PlayerController>().playerType;
        setCharacterImage(index);
    }

    void Update()
    {
        currentFill = backend.hp;
        maximum = backend.maxHP;
        fillMask.fillAmount = currentFill / maximum;
    }

}
