using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWheelUI : MonoBehaviour
{
    public Image[] normalWheel = new Image[4];
    public Image[] highlightWheel = new Image[4];

    private void Awake() 
    {
        for (int i=0;i<normalWheel.Length;i++)
        {
            normalWheel[i].gameObject.SetActive(false);
            highlightWheel[i].gameObject.SetActive(false);
        }
    }

    public void highlightWheelUI(int index)
    {
        normalWheel[index].gameObject.SetActive(false);
        highlightWheel[index].gameObject.SetActive(true);
    }

    public void normalizeWheelUI()
    {
        for (int i=0;i<normalWheel.Length;i++)
        {
            normalWheel[i].gameObject.SetActive(true);
            highlightWheel[i].gameObject.SetActive(false);
        }
    }
    public void hideWheelUI()
    {
        for (int i=0;i<normalWheel.Length;i++)
        {
            normalWheel[i].gameObject.SetActive(false);
            highlightWheel[i].gameObject.SetActive(false);
        }
    }
}
