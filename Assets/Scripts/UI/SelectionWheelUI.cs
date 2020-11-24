using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWheelUI : MonoBehaviour
{
    public Sprite[] normalWheel = new Sprite[4];
    public Sprite[] highlightWheel = new Sprite[4];
    public Image[] display = new Image[4];

    private void Awake() 
    {
        for (int i=0;i<normalWheel.Length;i++)
        {
           display[i].sprite = normalWheel[i];
        }
    }

    public void highlightWheelUI(int index)
    {
        gameObject.SetActive(true);
        display[index].sprite = highlightWheel[index];
    }

    public void normalizeWheelUI()
    {
        gameObject.SetActive(true);
        for (int i=0;i<normalWheel.Length;i++)
        {
           display[i].sprite = normalWheel[i];
        }
    }
    public void hideWheelUI()
    {
        normalizeWheelUI();
        gameObject.SetActive(false);
    }
}
