using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public Slider loadingBar;
    float u = 0.0f;
    public float lerpSpeed = 0.5f;
    private void Start()
    {
        u = 0.0f;
        loadingBar.value = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        u += Time.deltaTime*lerpSpeed;
        float v = Mathf.Lerp(0.0f,1.0f, u);
        loadingBar.value = v;
        if (u >= 1.0f)
            gameObject.SetActive(false);
    }
}
